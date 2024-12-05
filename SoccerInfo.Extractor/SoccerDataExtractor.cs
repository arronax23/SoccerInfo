using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using PuppeteerSharp;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Extractor.Parsers;

namespace SoccerInfo.Extractor
{
    public class SoccerDataExtractor(
        LeagueParser leagueParser,
        TeamParser teamParser,
        PlayerParser playerParser,
        PuppeteerManager puppeteerManager)
    {
        private readonly string BASE_URI = "https://www.transfermarkt.pl";

        public async Task<ExtractionDto> Extarct()
        {
            var extraction = new ExtractionDto();

            var leagueLinks = GetLeagueLinks();

            foreach (var leagueLink in leagueLinks)
            {
                var page = await puppeteerManager.InitializePage(headless: true);
                await page.GoToAsync(leagueLink, WaitUntilNavigation.Networkidle0);
                
                var leagueNode = await page.CreateHtmlNodeFromPage();
                var league = await leagueParser.Parse(leagueNode);

                var teamsNodes = leagueNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
                var teamsLinks = teamsNodes.Select(x => BASE_URI + x.GetAttributeValue("href", "Not found"));

                foreach (var teamLink in teamsLinks)
                {
                    await page.GoToAsync($"{teamLink}");
                    await page.WaitForNetworkIdleAsync();

                    var node = await page.CreateHtmlNodeFromPage();
                    league.Teams.Add(await GetTeam(node));
                }

                extraction.Leagues.Add(league);

                await puppeteerManager.CloseBrowser();
            }

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
                //TeamLinks.Ekstraklasa
            };
        }

        private async Task<TeamDto> GetTeam(HtmlNode node)
        {
            var team = await teamParser.Parse(node);

            var playersTableNode = node.QuerySelector("table.items");

            Traverser traverser = new Traverser();
            traverser.DFS(playersTableNode, EndSelectorsSpecification);
            var playersNodes = traverser.FoundNodes;

            foreach (var playerNode in playersNodes)
            {
                var player = await playerParser.Parse(playerNode);
                team.Players.Add(player);
            }

            return team;
        }

        private bool EndSelectorsSpecification(HtmlNode node)
        {
            return node.HasClass("odd") || node.HasClass("even");
        }
    }
}
