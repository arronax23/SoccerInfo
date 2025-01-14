using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;

namespace SoccerInfo.FrontendScraper.AcceptCookies;
public class CookiesExtractor(PlaywrightManager playwrightManager)
{
    public async Task Extract()
    {
        await playwrightManager.LaunchBrowser(headless: false);
        var page = await playwrightManager.Browser.NewPageAsync();
        await page.GotoAsync(Transfermarkt.BASE_URI);


        Console.WriteLine("Wait using breakpoint");
        Console.WriteLine("Accept cookies policy");

        var cookies = await page.GetCookies();
        await JsonSerializerToFile.Save(cookies, "./../SoccerInfo.FrontendScraper/Cookies.json");

        await playwrightManager.CloseBrowser();
    }
}
