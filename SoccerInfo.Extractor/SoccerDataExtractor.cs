using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using PuppeteerSharp;
using SoccerInfo.Extractor.Utilities;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Extractor.Parsers;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionData;

namespace SoccerInfo.Extractor
{
    public class SoccerDataExtractor(
        LeagueParser leagueParser,
        TeamParser teamParser,
        PlayerParser playerParser,
        NationalityParser nationalityImageParser,
        PuppeteerManager puppeteerManager)
    {
        private readonly string BASE_URI = "https://www.transfermarkt.pl";
        private readonly IList<Task> _extractionTasks = new List<Task>();

        public async Task<ExtractionData?> TryExtarct()
        {
            try
            {
                return await Extarct();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Extraction has been stopped by following exception:");  
                Console.WriteLine(ex.ToString());

                await puppeteerManager.CloseBrowser();

                return null;
            }
        }

        private async Task<ExtractionData> Extarct()
        {
            var extraction = new ExtractionData();

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
                TeamLinks.PremierLeague,
                //TeamLinks.Bundesliga,
                //TeamLinks.SerieA,
                //TeamLinks.LaLiga,
                //TeamLinks.Ligue1,
                //TeamLinks.LigaPortugal,
                //TeamLinks.JupilerProLeague,
                //TeamLinks.Eredivisie,
                //TeamLinks.SuperLig,
                //TeamLinks.Ekstraklasa,
                TeamLinks.SuperLeague1,
                TeamLinks.AustrianBundesliga
            };
        }

        private async Task GetLeague(ExtractionData extraction, string leagueLink)
        {
            IPage page;

            GoToLeague:
            try
            {
                page = await puppeteerManager.Browser.NewPageAsync();
                await page.GoToAsync(leagueLink, WaitUntilNavigation.Networkidle0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                goto GoToLeague;
            }


            var leagueNode = await page.CreateHtmlNodeFromPage();
            var league = await leagueParser.Parse(leagueNode);

            var teamsNodes = leagueNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
            var teamsLinks = teamsNodes.Select(x => BASE_URI + x.GetAttributeValue("href", "Not found"));

            foreach (var teamLink in teamsLinks)
            {
                GoToTeam:
                try
                {
                    await page.GoToAsync($"{teamLink}", WaitUntilNavigation.Networkidle0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
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
            var t_DFS = Benchmark.ExecuteAndGetTime(() => {
                Traverser traverser = new Traverser();
                traverser.DFS(playersTableNode, EndSelectorsSpecification);
                playersNodes = traverser.FoundNodes;
            },"DFS");

            var t_Q = Benchmark.ExecuteAndGetTime(() => {
                var odd = node.QuerySelectorAll(".odd");
                var even = node.QuerySelectorAll(".even");
                var results = odd.Union(even);
            }, "QuerySelector");


            if (t_DFS < t_Q)
                Console.WriteLine("DFS Win");
            else
                Console.WriteLine("QuerySelector win");


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
                player.NationalityImages.Add(nationalityImage);
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
}
