using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using RandomUserAgent;
using static SoccerInfo.FrontendScraper.Utilities.PlaywrightManager;

namespace SoccerInfo.FrontendScraper.Utilities;
public class PlaywrightManager(
    ILogger<PlaywrightManager> logger,
    IOptions<PlaywrightOptions> playwrightOptions)
{
    private IBrowser _browser = null!;
    private IBrowserContext _context = null!; 
    private IPlaywright _playwright = null!;

    public async Task LaunchBrowser(bool? headless = null)
    {
        try
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = headless ?? playwrightOptions.Value.Headless });
            _context = await _browser.NewContextAsync();
        }
        catch (Exception ex)
        {
            logger.LogError("Playwright LaunchBrowser Failed");
            logger.LogError(ex.ToString());
        }
    }

    [Obsolete]
    public async Task<IPage> NewPageWithRandomUserAgent()
    {
        string userAgent = RandomUa.RandomUserAgent;
        var ctx = await _browser.NewContextAsync(new() {UserAgent = userAgent });

        return await ctx.NewPageAsync();
    }

    public async Task<IPage> NewPage()
    {
        return await _context.NewPageAsync();
    }

    public async Task CloseBrowser()
    {
        await _browser.CloseAsync();
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }

    public class PlaywrightOptions
    {
        public bool Headless { get; set; }
    }
}
