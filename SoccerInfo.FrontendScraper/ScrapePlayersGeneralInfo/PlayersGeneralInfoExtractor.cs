using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using PuppeteerSharp;
using SoccerInfo.Shared.Utilities;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using Microsoft.Extensions.Logging;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo;

public class PlayersGeneralInfoExtractor(
    ILogger<PlayersGeneralInfoExtractor> logger,
    Traverser traverser,
    LeagueParser leagueParser,
    TeamParser teamParser,
    PlayerParser playerParser,
    NationalityParser nationalityImageParser,
    PuppeteerManager puppeteerManager)
{
    private readonly IList<Task> _extractionTasks = new List<Task>();

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

            await puppeteerManager.CloseBrowser();

            return null;
        }
    }

    private async Task<GeneralInfoExtractionData> Extarct()
    {
        var extraction = new GeneralInfoExtractionData();

        var leagueLinks = GetLeagueLinks();

        await puppeteerManager.LaunchBrowser(headless: true);
        foreach (var leagueLink in leagueLinks)
        {
            _extractionTasks.Add(GetLeague(extraction, leagueLink));
        }

        await Task.WhenAll(_extractionTasks);

        await puppeteerManager.CloseBrowser();
        return extraction;
    }


    private IEnumerable<string> GetLeagueLinks()
    {
        return new List<string>()
        {
            LeagueLink.PremierLeague,
            LeagueLink.Bundesliga,
            LeagueLink.SerieA,
            LeagueLink.LaLiga,
            LeagueLink.Ligue1,
            //LeagueLink.LigaPortugal,
            //LeagueLink.JupilerProLeague,
            //LeagueLink.Eredivisie,
            //LeagueLink.SuperLig,
            //LeagueLink.Ekstraklasa,
            //LeagueLink.SuperLeague1,
            //LeagueLink.AustrianBundesliga
        };
    }

    private async Task GetLeague(GeneralInfoExtractionData extraction, string leagueLink)
    {
        IPage page;

        Retry:
        try
        {
            page = await puppeteerManager.Browser.NewPageAsync();
            await page.GoToAsync(leagueLink, WaitUntilNavigation.Networkidle0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            goto Retry;
        }


        var leagueNode = await page.CreateHtmlNodeFromPage();
        var league = await leagueParser.Parse(leagueNode);

        var teamsNodes = leagueNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
        var teamsLinks = teamsNodes.Select(x => Transfermarkt.BASE_URI + x.GetAttributeValue("href", "Not found"));

        foreach (var teamLink in teamsLinks)
        {
            GoToTeam:
            try
            {
                await page.GoToAsync($"{teamLink}", WaitUntilNavigation.Networkidle0);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                goto GoToTeam;
            }

            var node = await page.CreateHtmlNodeFromPage();
            league.Teams.Add(await GetTeam(node));
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
            traverser.DFS(playersTableNode, EndSelectorsSpecification);
            playersNodes = traverser.FoundNodes;
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


        foreach (var playerNode in playersNodes)
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
            var nationalityImage = await GetNationalityImage(nationalityImageNode);
            player.Nationalities.Add(nationalityImage);
        }

        return player;
    }

    private async Task<NationalityData> GetNationalityImage(HtmlNode node)
    {
        return await nationalityImageParser.Parse(node);
    }

    private bool EndSelectorsSpecification(HtmlNode node)
    {
        return node.HasClass("odd") || node.HasClass("even");
    }
}
