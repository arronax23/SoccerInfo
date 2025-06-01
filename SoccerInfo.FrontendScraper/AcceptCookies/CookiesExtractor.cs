using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
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
        await page.GotoAsync(Transfermarkt.BASE_URI, new() 
        { 
            WaitUntil = WaitUntilState.NetworkIdle}
        );


        await page.EvaluateAsync(@"() => {
            const btn = [...document.querySelectorAll('button')].find(b => b.innerText.includes('Accept'));
            if (btn) btn.click();
        }");

        //await page.WaitForSelectorAsync("button[title=\'Accept & continue\']", new()
        //{
        //    Timeout = 7000
        //});

        var privacyFrame =page.Frames.Single(x => x.Url.StartsWith(@"https://cdn.privacy-mgmt.com"));
        var acceptButton = await privacyFrame.QuerySelectorAsync("button[title=\'Accept & continue\']");
        await acceptButton!.ClickAsync();


        var node = await page.CreateHtmlNodeFromPage();
        logger.LogInformation("Wait using breakpoint");
        logger.LogInformation("Accept cookies policy");

        var cookies = await page.GetCookies();
        await JsonSerializerToFile.Save(cookies, "./../SoccerInfo.FrontendScraper/Cookies.json", overwrite: true);

        await playwrightManager.CloseBrowser();
    }
}
