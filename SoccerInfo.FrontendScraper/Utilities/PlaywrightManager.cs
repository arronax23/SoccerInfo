using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager(ILogger<PlaywrightManager> logger)
{
    private IBrowser _browser = null!;
    private IPlaywright _playwright = null!;

    public IBrowser Browser => _browser;


    public async Task LaunchBrowser(bool headless)
    {
        try
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless });
        }
        catch (Exception ex)
        {
            logger.LogError("Playwright LaunchBrowser Failed");
        }
    }

    public async Task CloseBrowser()
    {
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
