using Microsoft.Extensions.Logging;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.FrontendScraper.AcceptCookies;
public class CookiesExtractor(
    ILogger<CookiesExtractor> logger,
    PlaywrightManager playwrightManager)
{
    public async Task Extract()
    {
        await playwrightManager.LaunchBrowser(headless: false);
        var page = await playwrightManager.NewPageWithRandomUserAgent();
        await page.GotoAsync(Transfermarkt.BASE_URI);


        logger.LogInformation("Wait using breakpoint");
        logger.LogInformation("Accept cookies policy");

        var cookies = await page.GetCookies();
        await JsonSerializerToFile.Save(cookies, "./../SoccerInfo.FrontendScraper/Cookies.json", overwrite: true);

        await playwrightManager.CloseBrowser();
    }
}
