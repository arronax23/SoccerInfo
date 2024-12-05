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

                var wholePageNode = await page.CreateHtmlNodeFromPage();
                var teamsNodes = wholePageNode.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
                var teamsLinks = teamsNodes.Select(x => BASE_URI + x.GetAttributeValue("href", "Not found"));

                foreach (var link in teamsLinks)
                {
                    await page.GoToAsync($"{link}");
                    await page.WaitForNetworkIdleAsync();

                    var node = await page.CreateHtmlNodeFromPage();
                    extraction.Leagues.Select(x => x.Teams);
                }

                await puppeteerManager.CloseBrowser();
            }

            return extraction;
        }


        private IEnumerable<string> GetLeagueLinks()
        {
            return new List<string>()
            {
                TeamLinks.PremierLeague,
                TeamLinks.Bundesliga,
                TeamLinks.SerieA,
                TeamLinks.LaLiga,
                TeamLinks.Ligue1,
                TeamLinks.LigaPortugal,
                TeamLinks.JupilerProLeague,
                TeamLinks.Eredivisie,
                TeamLinks.SuperLig,
                TeamLinks.Ekstraklasa
            };
        }

        private async Task Leagues(HtmlNode node)
        {

        }

            private async Task<TeamDto> GetTeamPlayers(HtmlNode node)
        {
            var teamDto  = new TeamDto();

            var tableNode = node.QuerySelector("table.items");

            var teamName = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedStrings();
            teamDto.TeamName = teamName;
            await Console.Out.WriteLineAsync(teamName);

            Traverser traverser = new Traverser();
            traverser.DFS(tableNode, EndSelectorsSpecification);
            var results = traverser.FoundNodes;

            foreach (var item in results)
            {
                var player  = await playerParser.Parse(item);
                teamDto.Players.Add(player);
            }

            return teamDto;
        }

        private bool EndSelectorsSpecification(HtmlNode node)
        {
            return node.HasClass("odd") || node.HasClass("even");
        }
    }
}
