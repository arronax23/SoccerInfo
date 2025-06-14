using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Polly.Registry;
using SoccerInfo.FrontendScraper.Resilience;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
using SoccerInfo.FrontendScraper.Utilities;
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
    public async Task<GeneralInfoExtractionData?> TryExtarct(IEnumerable<string> leagueLinks)
    {
        try
        {
            return await Extarct(leagueLinks);
        }
        catch (Exception ex)
        {
            logger.LogError("Extraction has been stopped by following exception:");
            logger.LogError(ex.ToString());

            await playwrightManager.CloseBrowser();

            return null;
        }
    }

    private async Task<GeneralInfoExtractionData> Extarct(IEnumerable<string> leagueLinks)
    {
        var extraction = new GeneralInfoExtractionData();

        await playwrightManager.LaunchBrowser();

        await Parallel.ForEachAsync(leagueLinks,
            new ParallelOptions { MaxDegreeOfParallelism = 2 },
            async (leagueLink, _) =>
            {
                await GetLeague(extraction, leagueLink);
            });

        await playwrightManager.CloseBrowser();
        return extraction;
    }

    private async Task GetLeague(GeneralInfoExtractionData extraction, string leagueLink)
    {
        IPage page = null!;
        LeagueData league = null!;
        IEnumerable<string> teamsLinks = null!;

        var leaguePipeline = pipelineProvider.GetPipeline(GeneralInfoExtractionPipeline.Name);

        await leaguePipeline.ExecuteAsync(async _ =>
        {
            try
            {
                page = await playwrightManager.NewPage();
                await page.GotoAsync(leagueLink, new PageGotoOptions() { WaitUntil = WaitUntilState.NetworkIdle });

                HtmlNode leagueNode = await page.CreateHtmlNodeFromPage();
                league = await leagueParser.Parse(leagueNode);

                IList<HtmlNode> teamsNodes = leagueNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
                teamsLinks = teamsNodes.Select(x => Transfermarkt.BASE_URI + x.GetAttributeValue("href", "Not found"));
            }
            catch (Exception ex)
            {
                logger.LogError($"League link: {leagueLink}");
                logger.LogError(ex.ToString());
                throw;
            }
        });

        await GetTeams(extraction, page, teamsLinks, league);
    }

    private async Task GetTeams(GeneralInfoExtractionData extraction, IPage page, IEnumerable<string> teamsLinks, LeagueData league)
    {
        await Parallel.ForEachAsync(teamsLinks,
            new ParallelOptions { MaxDegreeOfParallelism = 1 },
            async (teamLink, _) =>
            {
                var teamPipeline = pipelineProvider.GetPipeline(GeneralInfoExtractionPipeline.Name);

                await teamPipeline.ExecuteAsync(async _ =>
                {
                    try
                    {
                        await page.GotoAsync($"{teamLink}", new PageGotoOptions() { WaitUntil = WaitUntilState.DOMContentLoaded });

                        var node = await page.CreateHtmlNodeFromPage();
                        league.Teams.Add(await GetTeam(node));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Team link: {teamLink}");
                        logger.LogError(ex.ToString());
                        throw;
                    }
                });
            });

        extraction.Leagues.Add(league);
    }

    private async Task<TeamData> GetTeam(HtmlNode node)
    {
        var team = await teamParser.Parse(node);
        var playersTableNode = node.QuerySelector("table.items");
        var playersNodes = traverser.Search(playersTableNode, EndSelectorsSpecification);

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
