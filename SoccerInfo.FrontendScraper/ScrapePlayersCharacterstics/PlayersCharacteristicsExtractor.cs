using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using HtmlAgilityPack;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Playwright;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;
using SoccerInfo.FrontendScraper.Resilience;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;

public class PlayersCharacteristicsExtractor(
    ILogger<PlayersCharacteristicsExtractor> logger,
    ResiliencePipelineProvider<string> pipelineProvider,
    PlaywrightManager playwrightManager,
    CookieReader cookieReader,
    InfoTableParser infoTableParser,
    NationalTeamParser nationalTeamParser,
    SocialsParser socialsParser,
    StatsParser statsParser)
{
    public async Task<PlayersCharacteristicsExtractionData?> TryExtract(
        IEnumerable<PlayerExtractionData> playersExtraction,
        CancellationToken cancellationToken)
    {
        try
        {
            return await Extarct(playersExtraction, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError("Extraction has been stopped by following exception:");
            logger.LogError(ex.ToString());

            await playwrightManager.CloseBrowser();

            return null;
        }
    }

    private async Task<PlayersCharacteristicsExtractionData> Extarct(
        IEnumerable<PlayerExtractionData> playersExtraction,
        CancellationToken cancellationToken)
    {
        var playersCharacteristicsExtraction = new PlayersCharacteristicsExtractionData();

        await playwrightManager.LaunchBrowser();

        int playersScrapedCount = 0;

        try
        {
            await Parallel.ForEachAsync(playersExtraction,
                new ParallelOptions { MaxDegreeOfParallelism = 4 },
                async (playerExtraction, cancellationToken) =>
                {
                    try
                    {
                        await GetPlayer(playerExtraction, playersCharacteristicsExtraction, cancellationToken);
                        logger.LogInformation($"Scraped players: {++playersScrapedCount}/{playersExtraction.Count()}");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Error processing player \nId:{playerExtraction.TransfermarktId} \nURL:{playerExtraction.TransfermarktURL}: {ex}");
                    }
                });
        }
        finally
        {
            await playwrightManager.CloseBrowser();
        }

        return playersCharacteristicsExtraction;
    }

    private async Task GetPlayer(PlayerExtractionData playerExtraction, PlayersCharacteristicsExtractionData playersCharacteristicsExtractionData, CancellationToken cancellationToken)
    {
        var pipeline = pipelineProvider.GetPipeline(CharacteristicsExtractionPipeline.Name);

        await pipeline.ExecuteAsync(async cancellationToken =>
        {
            IPage page = null!;

            try
            {
                page = await playwrightManager.NewPageWithRandomUserAgent();
                await page.Context.AddCookiesAsync(cookieReader.ReadFromJsonFile());

                await page.GotoAsync(Transfermarkt.BASE_URI + playerExtraction.TransfermarktURL, new PageGotoOptions()
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded
                });

                await page.ScrollToBottomAsync();
                bool isStatsGridAvailable = await page.TryWaitForSelectorAsync(".grid-table", 10_000);

                var rootNode = await page.CreateHtmlNodeFromPage();

                playersCharacteristicsExtractionData.PlayersCharacteristics.Add(
                    await ParsePlayer(rootNode, playerExtraction.TransfermarktId, playerExtraction.isGoalkeeper, isStatsGridAvailable));

                await page.CloseAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                await page.CloseAsync();
                throw;
            }
        }, cancellationToken);
    }


    private async Task<PlayerCharacteristicsData> ParsePlayer(HtmlNode rootNode,int transfermarktId, bool isGoalkeeper, bool isStatsGridAvailable)
    {
        var playerCharacteristicsData = new PlayerCharacteristicsData(transfermarktId, isGoalkeeper);

        var infoTableNode = rootNode.QuerySelector(".info-table");
        infoTableParser.Parse(infoTableNode, playerCharacteristicsData);

        var nationalTeamNode = rootNode.QuerySelector(".data-header__info-box .data-header__details").QuerySelectorAll("ul").Last();
        nationalTeamParser.Parse(nationalTeamNode, playerCharacteristicsData);

        var socialMediaIconsNode = rootNode.QuerySelector(".social-media-toolbar__icons");
        socialsParser.Parse(socialMediaIconsNode, playerCharacteristicsData);

        if (isStatsGridAvailable)
        {
            var gridTableNode = rootNode.QuerySelector(".grid-table");
            await statsParser.Parse(gridTableNode, playerCharacteristicsData);
        }

        return playerCharacteristicsData;
    }
}
