using HtmlAgilityPack;
using Microsoft.Playwright;
using Serilog;

namespace SoccerInfo.FrontendScraper.Utilities;

public static class PlaywrightExtensions
{
    public static async Task<IEnumerable<Cookie>> GetCookies(this IPage page)
    {
        return (await page.Context.CookiesAsync()).Select(c => new Cookie
        {
            Name = c.Name,
            Value = c.Value,
            Domain = c.Domain,
            Path = c.Path,
            Expires = c.Expires,
            HttpOnly = c.HttpOnly,
            Secure = c.Secure,
            SameSite = c.SameSite,
        });
    }

    //public static async Task ScrollToBottomAsync(this IPage page, int delayMs = 50, int scrollHeightIncrement = 200)
    //{
    //    try
    //    {
    //        await page.EvaluateAsync(@"async ([delayMs, scrollHeightIncrement]) => {
    //        const delay = ms => new Promise(resolve => setTimeout(resolve, ms));
    //        while (document.scrollingElement.scrollTop + window.innerHeight < document.scrollingElement.scrollHeight) {
    //            document.scrollingElement.scrollTop += scrollHeightIncrement;
    //            await delay(delayMs);
    //        }
    //    }", new[] {delayMs, scrollHeightIncrement});
    //    }
    //    catch (Exception)
    //    {
    //        Log.Logger.Error("scrolling exception");
    //    }

    //}


    public static async Task ScrollToBottomAsync(this IPage page, int delayMs = 50, int scrollHeightIncrement = 200, int maxScrollAttempts = 25)
    {
        try
        {
            await page.EvaluateAsync(@"async ([delayMs, scrollHeightIncrement, maxScrollAttempts]) => {
            const delay = ms => new Promise(resolve => setTimeout(resolve, ms));
            let attempts = 0;
            while (document.scrollingElement.scrollTop + window.innerHeight < document.scrollingElement.scrollHeight) {
                document.scrollingElement.scrollTop += scrollHeightIncrement;
                await delay(delayMs);
                attempts++;
                if (attempts > maxScrollAttempts) break;
            }
        }", new[] { delayMs, scrollHeightIncrement, maxScrollAttempts });
        }
        catch (Exception)
        {
            Log.Logger.Error("scrolling exception");
        }

    }

    public static async Task<bool> TryWaitForSelectorAsync(this IPage page, string selector, int timeout)
    {
        try
        {
            await page.WaitForSelectorAsync(selector, new() { Timeout = timeout });
            return true;
        }
        catch (Exception ex)
        {
            Log.Logger.Warning(ex.ToString());
            return false;
        }
    }

    public static async Task<HtmlNode> CreateHtmlNodeFromPage(this IPage page)
        => HtmlNode.CreateNode(await page.ContentAsync());


    public static async Task AcceptCookiesIfNeeded(this IPage page)
    {

        try
        {
            await Task.Delay(1_000);
            var frame = page.Frames.SingleOrDefault(x => x.Url.StartsWith(@"https://cdn.privacy-mgmt.com"));
            
            if (frame is null)
                return;

            var acceptButton = await frame.QuerySelectorAsync("button[title=\'Accept & continue\']");
            await acceptButton!.ClickAsync();
            await Task.Delay(1_000);
            Log.Logger.Information("Click on accept cookies button - SUCCESSFUL");

        }
        catch (Exception ex)
        {
            Log.Logger.Warning("Click on accept cookies button - FAILED");
            Log.Logger.Warning(ex.ToString());
        }
    }
}
