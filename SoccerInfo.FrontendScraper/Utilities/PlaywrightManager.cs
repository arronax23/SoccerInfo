using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager
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
            Console.WriteLine("Playwright LaunchBrowser Failed");
        }
    }

    public async Task CloseBrowser()
    {
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
