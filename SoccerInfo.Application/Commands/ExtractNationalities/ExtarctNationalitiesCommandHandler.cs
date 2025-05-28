using Microsoft.Playwright;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.CQRS;
using Microsoft.Extensions.Logging;
using Polly.Registry;

namespace SoccerInfo.Application.Commands.ExtractNationalities;

internal class ExtarctNationalitiesCommandHandler(
    PlaywrightManager playwrightManager,
    CookieReader cookieReader,
    ILogger<ExtarctNationalitiesCommandHandler> logger,
    ResiliencePipelineProvider<string> pipelineProvider) : ICommandHandler<ExtarctNationalitiesCommand>
{
    private readonly string _outputFilePath = "nationalities.txt";

    public async Task Handle(ExtarctNationalitiesCommand request, CancellationToken cancellationToken)
    {
        await playwrightManager.LaunchBrowser();
        var page = await playwrightManager.NewPageWithRandomUserAgent();
        await page.Context.AddCookiesAsync(cookieReader.ReadFromJsonFile());

        await page.GotoAsync("https://www.transfermarkt.com/marktwertetop/wertvollstenationalmannschaften", new PageGotoOptions()
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });

        bool continueSearching = true;
        while (continueSearching)
        {

            try
            {
                await GetNationalitiesFromPage(page);
                await page.WaitForSelectorAsync(".tm-pagination__list-item--icon-next-page a");
                await page.ClickAsync(".tm-pagination__list-item--icon-next-page a");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                continueSearching =  false;
            }
        }

       await playwrightManager.CloseBrowser();
    }


    private async Task GetNationalitiesFromPage(IPage page)
    {
        var node = await page.CreateHtmlNodeFromPage();
        var aTags = node.QuerySelectorAll(".odd, .even")
                .Select(container => container.QuerySelector("a"))
                .Where(a => a != null)
                .ToList();

        List<string> nationalityLinks = new();

        await Parallel.ForEachAsync(aTags,
            new ParallelOptions { MaxDegreeOfParallelism = 1 },
            async (a, _) =>
            {
                var link = a.GetAttributeValue("href", "notFound");
                if (link != "notFound")
                    await GetNationality(link);

            });
    }

    private async Task GetNationality(string nationalityLink)
    {
        var resiliencePipeline = pipelineProvider.GetPipeline(NationalityExtractionPipeline.Name);

        await resiliencePipeline.ExecuteAsync(async _ =>
        {
            var page = await playwrightManager.NewPageWithRandomUserAgent();
            await page.GotoAsync("https://www.transfermarkt.com" + nationalityLink, new()
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

            await page.WaitForSelectorAsync(".selector-title");

            var node = await page.CreateHtmlNodeFromPage();

            var nationality = node.QuerySelector(".selector-title").InnerText.Trim();
            await File.AppendAllTextAsync(_outputFilePath, nationality + Environment.NewLine);
            logger.LogInformation($"Extracted nationality: {nationality}");
            await page.CloseAsync();
        });
    }
}
