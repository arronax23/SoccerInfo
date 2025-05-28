using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using RandomUserAgent;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager(
    ILogger<PlaywrightManager> logger,
    IConfiguration configuration)
{
    private IBrowser _browser = null!;
    private IPlaywright _playwright = null!;
    public PlaywrightSettings Settings => new(configuration);


    public async Task LaunchBrowser(bool? headless = null)
    {
        try
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless ?? Settings.Headless });
        }
        catch (Exception ex)
        {
            logger.LogError("Playwright LaunchBrowser Failed");
            logger.LogError(ex.ToString());
        }
    }


    public async Task<IPage> NewPageWithRandomUserAgent()
    {
        string userAgent = RandomUa.RandomUserAgent;
        var ctx = await _browser.NewContextAsync(new() {UserAgent = userAgent });

        return await ctx.NewPageAsync();
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
