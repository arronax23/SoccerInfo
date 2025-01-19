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

    public static async Task ScrollToBottomAsync(this IPage page, int delayMs = 50, int scrollHeightIncrement = 200)
    {
        try
        {
            await page.EvaluateAsync(@"async ([delayMs, scrollHeightIncrement]) => {
            const delay = ms => new Promise(resolve => setTimeout(resolve, ms));
            while (document.scrollingElement.scrollTop + window.innerHeight < document.scrollingElement.scrollHeight) {
                document.scrollingElement.scrollTop += scrollHeightIncrement;
                await delay(delayMs);
            }
        }", new[] {delayMs, scrollHeightIncrement});
        }
        catch (Exception)
        {
            Log.Logger.Error("scrolling exception");
        }

    }
    public static async Task<HtmlNode> CreateHtmlNodeFromPage(this IPage page)
        => HtmlNode.CreateNode(await page.ContentAsync());
}
