using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Polly.Registry;
using SoccerInfo.FrontendScraper.Resilience;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics;

public class PlayersCharacteristicsExtractor(
    ILogger<PlayersCharacteristicsExtractor> logger,
    ResiliencePipelineProvider<string> pipelineProvider,
    PlaywrightManager playwrightManager,
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
        //await AcceptCookies();

        int playersScrapedCount = 0;

        await Parallel.ForEachAsync(playersExtraction,
            new ParallelOptions { MaxDegreeOfParallelism = 4 },
            async (playerExtraction, cancellationToken) =>
            {
                await GetPlayer(playerExtraction, playersCharacteristicsExtraction, cancellationToken);
                logger.LogInformation($"Scraped players: {++playersScrapedCount}/{playersExtraction.Count()}");
            });

        await playwrightManager.CloseBrowser();

        return playersCharacteristicsExtraction;
    }

    [Obsolete]
    private async Task AcceptCookies()
    {
        IPage page = await playwrightManager.NewPage();
        await page.GotoAsync(Transfermarkt.BASE_URI, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        await page.AcceptCookiesIfNeeded();
    }

    private async Task GetPlayer(PlayerExtractionData playerExtraction, PlayersCharacteristicsExtractionData playersCharacteristicsExtractionData, CancellationToken cancellationToken)
    {
        var pipeline = pipelineProvider.GetPipeline(CharacteristicsExtractionPipeline.Name);

        await pipeline.ExecuteAsync(async cancellationToken =>
        {
            IPage page = null!;

            try
            {
                page = await playwrightManager.NewPage();

                var response = await page.GotoAsync(Transfermarkt.BASE_URI + playerExtraction.TransfermarktURL, new PageGotoOptions()
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                });

                await page.AcceptCookiesIfNeeded();

                if (!response!.Ok)
                    throw new Exception($"Page GotoAsync() returned Http Response: {response.Status.ToString()}");

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

        var nationalTeamNode = rootNode.QuerySelector(".data-header__info-box .data-header__details")?.QuerySelectorAll("ul")?.Last();
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
