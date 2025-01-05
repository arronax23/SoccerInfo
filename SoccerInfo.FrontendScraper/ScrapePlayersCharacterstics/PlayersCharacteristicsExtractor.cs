using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using HtmlAgilityPack;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Playwright;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;

public class PlayersCharacteristicsExtractor(
    PlaywrightManager playwrightManager,
    CookieReader cookieReader,
    InfoTableParser infoTableParser,
    NationalTeamParser nationalTeamParser,
    SocialsParser socialsParser,
    StatsParser statsParser)
{
    public async Task<PlayersCharacteristicsExtractionData?> TryExtract(IEnumerable<PlayerExtractionData> playersExtraction)
    {
        try
        {
            return await Extarct(playersExtraction);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Extraction has been stopped by following exception:");
            Console.WriteLine(ex.ToString());

            await playwrightManager.CloseBrowser();

            return null;
        }
    }

    private async Task<PlayersCharacteristicsExtractionData> Extarct(IEnumerable<PlayerExtractionData> playersExtraction)
    {
        var playersCharacteristicsExtraction = new PlayersCharacteristicsExtractionData();
 
        await playwrightManager.LaunchBrowser(headless: false);

        try
        {
            await Parallel.ForEachAsync(playersExtraction,
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                async (playerExtraction, cancellationToken) =>
                {
                    try
                    {
                        await GetPlayer(playerExtraction, playersCharacteristicsExtraction);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing player \nId:{playerExtraction.TransfermarktId} \nURL:{playerExtraction.TransfermarktURL}: {ex}");
                    }
                });
        }
        finally
        {
            await playwrightManager.CloseBrowser();
        }

        return playersCharacteristicsExtraction;
    }

    private async Task GetPlayer(PlayerExtractionData playerExtraction, PlayersCharacteristicsExtractionData playersCharacteristicsExtractionData)
    {
        IPage? page = null;

        Retry:
        try
        {
            page = await playwrightManager.Browser.NewPageAsync();
            await page.Context.AddCookiesAsync(cookieReader.ReadFromJsonFile());

            await page.GotoAsync(Transfermarkt.BASE_URI + playerExtraction.TransfermarktURL, new PageGotoOptions()
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

            await page.ScrollToBottomAsync();
            await page.WaitForSelectorAsync(".grid-table");

            //var cookies = await page.GetCookies();
            //await JsonSerializerToFile.Save(cookies, "./../SoccerInfo.FrontendScraper/Cookies.json");

            var rootNode = await page.CreateHtmlNodeFromPage();

            playersCharacteristicsExtractionData.PlayersCharacteristics.Add(
                await ParsePlayer(rootNode, playerExtraction.TransfermarktId, playerExtraction.isGoalkeeper));

            await page.CloseAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            await page!.CloseAsync();
            goto Retry;
        }
    }


    private async Task<PlayerCharacteristicsData> ParsePlayer(HtmlNode rootNode,int transfermarktId, bool isGoalkeeper)
    {
        var playerCharacteristicsData = new PlayerCharacteristicsData(transfermarktId, isGoalkeeper);

        var infoTableNode = rootNode.QuerySelector(".info-table");
        infoTableParser.Parse(infoTableNode, playerCharacteristicsData);

        var nationalTeamNode = rootNode.QuerySelector(".data-header__info-box .data-header__details").QuerySelectorAll("ul").Last();
        nationalTeamParser.Parse(nationalTeamNode, playerCharacteristicsData);

        var socialMediaIconsNode = rootNode.QuerySelector(".social-media-toolbar__icons");
        socialsParser.Parse(socialMediaIconsNode, playerCharacteristicsData);

        var gridTableNode = rootNode.QuerySelector(".grid-table");
        await statsParser.Parse(gridTableNode, playerCharacteristicsData);

        return playerCharacteristicsData;
    }
}
