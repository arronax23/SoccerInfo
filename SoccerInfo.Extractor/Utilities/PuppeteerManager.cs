using Microsoft.Extensions.Logging;
using PuppeteerSharp;

namespace SoccerInfo.FrontendScraper.Utilities;

public class PuppeteerManager(ILogger<PuppeteerManager> logger)
{
    private IBrowser _browser = null!;
    private IPage _page = null!;

    public IBrowser Browser => _browser;

    public async Task<IPage> InitializePage(bool headless)
    {
        await LaunchBrowser(headless);
        var pages = await _browser.PagesAsync();
        _page = pages[0];

        await _page.SetViewportAsync(new ViewPortOptions
        {
            Width = 1600,
            Height = 900,
        });

        return _page;
    }

    public async Task LaunchBrowser(bool headless)
    {
        try
        {
            _browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = headless });
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Catched exception:\n" + ex.Message);
            logger.LogInformation("Start Chrome download");
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            _browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = headless });
        }
    }

    public async Task CloseBrowser()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }
}
