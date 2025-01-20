using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager(IHostEnvironment hostEnvironment, ILogger<PlaywrightManager> logger)
{
    private IBrowser _browser = null!;
    private IPlaywright _playwright = null!;

    public IBrowser Browser => _browser;


    public async Task LaunchBrowser(bool headless)
    {
        try
        {
            _playwright = await Playwright.CreateAsync();


            if (hostEnvironment.IsProduction())
            {
                var rootPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                _browser = await _playwright.Chromium.LaunchAsync(new() 
                { 
                    Headless = headless,
                    ExecutablePath = Path.Combine(rootPath, ".cache/ms-playwright/chromium_headless_shell-1148/chrome-linux")
                });
            }
            else
                _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless });
        }
        catch (Exception ex)
        {
            logger.LogError("Playwright LaunchBrowser Failed");
            logger.LogError(ex.ToString());
        }
    }

    public async Task CloseBrowser()
    {
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
