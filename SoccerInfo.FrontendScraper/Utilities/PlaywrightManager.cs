using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager(
    ILogger<PlaywrightManager> logger,
    IConfiguration configuration)
{
    private IBrowser _browser = null!;
    private IPlaywright _playwright = null!;

    public IBrowser Browser => _browser;
    public PlaywrightSettings Settings => new(configuration);


    public async Task LaunchBrowser(bool? headless = null)
    {
        try
        {
            _playwright = await Playwright.CreateAsync();

            //if (hostEnvironment.IsProduction())
            //{
            //    var rootPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //    _browser = await _playwright.Chromium.LaunchAsync(new() 
            //    { 
            //        Headless = headless,
            //        ExecutablePath = Path.Combine(rootPath, ".cache/ms-playwright/chromium_headless_shell-1148/chrome-linux")
            //    });
            //}
            //else
            //    _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless });


            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless ?? Settings.Headless });
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

    public class PlaywrightSettings(IConfiguration configuration)
    {
        public bool Headless => configuration.GetValue<bool>("PlaywrightSettings:Headless");
    }
}
