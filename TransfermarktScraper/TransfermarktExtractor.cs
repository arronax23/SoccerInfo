using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using PuppeteerSharp;
using TransfermarktScraper.Utilities;
using static TransfermarktScraper.Dto.PlayersExtractionDto;
using TransfermarktScraper.Dto;

namespace TransfermarktScraper
{
    public class TransfermarktExtractor(
        PuppeteerManager puppeteerManager,
        ImageFetcher imageFetcher)
    {
        private readonly string BASE_URI = "https://www.transfermarkt.pl";
        public async Task<PlayersExtractionDto> Extarct() {

            var extraction = new PlayersExtractionDto();

            var page = await puppeteerManager.InitializePage(headless: true);
            await page.GoToAsync("https://www.transfermarkt.pl/premier-league/startseite/wettbewerb/GB1", WaitUntilNavigation.Networkidle0);
            var n = await page.CreateHtmlNodeFromPage();
            var teams = n.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
            var teamsLinks = teams.Select(x => BASE_URI + x.GetAttributeValue("href", "Not found"));

            foreach (var link in teamsLinks)
            {
                await page.GoToAsync($"{link}");
                await page.WaitForNetworkIdleAsync();
                var node = await page.CreateHtmlNodeFromPage();
                extraction.Extraction.Add(await GetTeamPlayers(node));
            }

            await puppeteerManager.CloseBrowser();

            return extraction;
        }

        private async Task<TeamPlayersDto> GetTeamPlayers(HtmlNode node)
        {
            var teamPlayersDto  = new TeamPlayersDto();

            var tableNode = node.QuerySelector("table.items");

            var teamName = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedStrings();
            teamPlayersDto.TeamName = teamName;
            await Console.Out.WriteLineAsync(teamName);

            Traverser traverser = new Traverser();
            traverser.DFS(tableNode, EndSelectorsSpecification);
            var results = traverser.FoundNodes;

            foreach (var item in results)
            {
                var player = new PlayerDto()
                {
                    Name = item.QuerySelector(".hauptlink").InnerText.FormatExtractedStrings(),
                    Position = item.QuerySelectorAll("tr").Last().InnerText.FormatExtractedStrings(),
                    FaceImageBase64 = await imageFetcher.Fetch(
                        item.QuerySelector("img.bilderrahmen-fixed").GetAttributeValue("data-src", "notFound"))
                };   

                foreach(var flag in item.QuerySelectorAll("img.flaggenrahmen"))
                {
                    player.NationalityImageBase64Collection.Add(
                        await imageFetcher.Fetch(flag.GetAttributeValue("src", "notFound")));
                }

                teamPlayersDto.Players.Add(player);
            }

            return teamPlayersDto;
        }

        private bool EndSelectorsSpecification(HtmlNode node)
        {
            return node.HasClass("odd") || node.HasClass("even");
        }
    }
}
