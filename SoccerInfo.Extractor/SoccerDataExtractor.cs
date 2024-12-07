using HtmlAgilityPack.CssSelectors.NetCore;
using HtmlAgilityPack;
using PuppeteerSharp;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Extractor.Parsers;
using System.Collections.Generic;

namespace SoccerInfo.Extractor
{
    public class SoccerDataExtractor(
        LeagueParser leagueParser,
        TeamParser teamParser,
        PlayerParser playerParser,
        NationalityImageParser nationalityImageParser,
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
            }

            await puppeteerManager.CloseBrowser();
            return extraction;
        }


        private IEnumerable<string> GetLeagueLinks()
        {
            return new List<string>()
            {
                TeamLinks.PremierLeague,
                TeamLinks.Bundesliga,
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
                team.Players.Add(await GetPlayer(playerNode));
            }

            return team;
        }

        private async Task<PlayerDto> GetPlayer(HtmlNode node)
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

        private async Task<NationalityImageDto> GetNationalityImage(HtmlNode node)
        {
            return await nationalityImageParser.Parse(node);
        }

        private bool EndSelectorsSpecification(HtmlNode node)
        {
            return node.HasClass("odd") || node.HasClass("even");
        }
    }
}
