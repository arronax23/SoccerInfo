using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using SoccerInfo.Shared.Utilities;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using SoccerInfo.FrontendScraper.Resilience;
using Microsoft.Playwright;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;

public class PlayersGeneralInfoExtractor(
    ILogger<PlayersGeneralInfoExtractor> logger,
    Traverser traverser,
    LeagueParser leagueParser,
    TeamParser teamParser,
    PlayerParser playerParser,
    NationalityParser nationalityImageParser,
    PlaywrightManager playwrightManager,
    ResiliencePipelineProvider<string> pipelineProvider)
{
    public async Task<GeneralInfoExtractionData?> TryExtarct()
    {
        try
        {
            return await Extarct();
        }
        catch (Exception ex)
        {
            logger.LogError("Extraction has been stopped by following exception:");
            logger.LogError(ex.ToString());

            await playwrightManager.CloseBrowser();

            return null;
        }
    }

    private async Task<GeneralInfoExtractionData> Extarct()
    {
        var extraction = new GeneralInfoExtractionData();
        var leagueLinks = GetLeagueLinks();

        await playwrightManager.LaunchBrowser();

        await Parallel.ForEachAsync(leagueLinks,
            new ParallelOptions { MaxDegreeOfParallelism = 4 },
            async (leagueLink, _) =>
            {
                await GetLeague(extraction, leagueLink);
            });

        await playwrightManager.CloseBrowser();
        return extraction;
    }


    private IEnumerable<string> GetLeagueLinks()
    {
        return new List<string>()
        {
            //LeagueLink.PremierLeague,
            //LeagueLink.Bundesliga,
            //LeagueLink.SerieA,
            //LeagueLink.LaLiga,
            //LeagueLink.Ligue1,

            //LeagueLink.LigaPortugal,
            //LeagueLink.JupilerProLeague,
            //LeagueLink.Eredivisie,
            //LeagueLink.SuperLig,
            //LeagueLink.SuperLeague1,
            //LeagueLink.SuperLeague,
            //LeagueLink.Ekstraklasa,
            //LeagueLink.AustrianBundesliga,
            //LeagueLink.BrazilSerieA,
            LeagueLink.SaudiProLeague,
            //LeagueLink.MLS,
        };
    }

    private async Task GetLeague(GeneralInfoExtractionData extraction, string leagueLink)
    {
        var leaguePipeline = pipelineProvider.GetPipeline(GeneralInfoExtractionPipeline.Name);

        IPage page = null!;
        await leaguePipeline.ExecuteAsync(async _ =>
        {
            try
            {
                page = await playwrightManager.Browser.NewPageAsync();
                await page.GotoAsync(leagueLink, new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        });

        var leagueNode = await page.CreateHtmlNodeFromPage();
        var league = await leagueParser.Parse(leagueNode);

        var teamsNodes = leagueNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
        var teamsLinks = teamsNodes.Select(x => Transfermarkt.BASE_URI + x.GetAttributeValue("href", "Not found"));

        foreach (var teamLink in teamsLinks)
        {
            var teamPipeline = pipelineProvider.GetPipeline(GeneralInfoExtractionPipeline.Name);
            await teamPipeline.ExecuteAsync(async _ =>
            {
                try
                {
                    await page.GotoAsync($"{teamLink}", new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });

                    var node = await page.CreateHtmlNodeFromPage();
                    league.Teams.Add(await GetTeam(node));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.ToString());
                    throw;
                }
            });
        }

        extraction.Leagues.Add(league);
        
    }

    private async Task<TeamData> GetTeam(HtmlNode node)
    {
        var team = await teamParser.Parse(node);

        var playersTableNode = node.QuerySelector("table.items");

        IReadOnlyCollection<HtmlNode>? playersNodes = null;
        var t_DFS = Benchmark.ExecuteAndGetTime(() =>
        {
            playersNodes = traverser.Search(playersTableNode, EndSelectorsSpecification);
        }, "DFS");

        var t_Q = Benchmark.ExecuteAndGetTime(() =>
        {
            var odd = node.QuerySelectorAll(".odd");
            var even = node.QuerySelectorAll(".even");
            var results = odd.Union(even);
        }, "QuerySelector");


        if (t_DFS < t_Q)
            logger.LogInformation("DFS Win");
        else
            logger.LogInformation("QuerySelector win");


        foreach (var playerNode in playersNodes!.ToList())
        {
            team.Players.Add(await GetPlayer(playerNode));
        }

        return team;
    }

    private async Task<PlayerData> GetPlayer(HtmlNode node)
    {
        var player = await playerParser.Parse(node);
        var nationalityImageNodes = node.QuerySelectorAll("img.flaggenrahmen");

        foreach (var nationalityImageNode in nationalityImageNodes)
        {
            var nationalityImage = await GetNationality(nationalityImageNode);
            player.Nationalities.Add(nationalityImage);
        }

        return player;
    }

    private async Task<NationalityData> GetNationality(HtmlNode node)
    {
        return await nationalityImageParser.Parse(node);
    }

    private bool EndSelectorsSpecification(HtmlNode node)
    {
        return node.HasClass("odd") || node.HasClass("even");
    }
}
