using HtmlAgilityPack;
using PuppeteerSharp;
namespace TransfermarktScraper;

public static class PageExtensions
{
    private static int counter = 0;
    public static async Task<HtmlNode> CreateHtmlNodeFromPage(this IPage page)
    {
        return HtmlNode.CreateNode(await page.GetContentAsync());
    }

    public static async Task<IElementHandle?> QuerySingleWithWait(this IPage page, string selector, int? timeout = 1000)
    {
        try
        {
            await page.WaitForSelectorAsync(selector, new WaitForSelectorOptions() { Timeout = timeout });
            return await page.QuerySelectorAsync(selector);
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public static async Task<IElementHandle[]?> QueryAllWithWait(this IPage page, string selector, int? timeout = 1000)
    {
        try
        {
            await page.WaitForSelectorAsync(selector, new WaitForSelectorOptions() { Timeout = timeout });
            return await page.QuerySelectorAllAsync(selector);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static async Task<bool> Wait(this IPage page, string selector, Func<Task>? action = null, int? timeout = 1000)
    {
        try
        {
            await page.WaitForSelectorAsync(selector, new WaitForSelectorOptions() { Timeout = timeout });
            return true;
        }
        catch (Exception ex)
        {

            if (action != null)
            {
                await action();
            }

            return false;
        }
    }

    //public static async Task<bool> ClickWithWait(this IPage page, string clickSelector, string waitTargetNode, int? timeout = 5000)
    //{
    //    try
    //    {
    //        await Benchmark.ExecuteAndMeasureTimeAsync(async () => await page.WaitForSelectorAsync(clickSelector, new WaitForSelectorOptions() { Timeout = timeout }), "Change page wait");

    //        await page.ClickAsync(clickSelector);
    //        await Benchmark.ExecuteAndMeasureTimeAsync(async () =>
    //        {
    //            await page.WaitForMuatations(waitTargetNode, 10, 5, timeout);
    //        }, "WaitForMuatations");
    //        Log.Logger.Information("Counter: " + (++counter).ToString());
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Logger.Information(ex.Message);
    //        return false;
    //    }
    //}

    private static async Task WaitForMuatations(this IPage page, string targetNode, int idleCountNeeded, int intervalDurationMilliseconds, int? timeout)
    {
        await page.EvaluateFunctionAsync(@"(targetNode, idleCountNeeded, intervalDurationMilliseconds) => {
            let callbackCounter = 0;
            let prevcallbackCounter = 0;
            let idleCounter = 0;

            const check = () => {
                if (callbackCounter > prevcallbackCounter){
                    prevcallbackCounter = callbackCounter;
                    idleCounter = 0;
                    setTimeout(check, intervalDurationMilliseconds);
                }
                else if (callbackCounter == prevcallbackCounter){
                    if (callbackCounter > 0){
                        idleCounter++;
                    }

                    if (idleCounter >= idleCountNeeded){
                        window.finished = true;
                    }
                    else {
                        setTimeout(check, intervalDurationMilliseconds);
                    }
                }
            }

            check();

            const callback = function (mutations) {
                ++callbackCounter;
            };
    
            const mo = new MutationObserver(callback);
    
            const node = document.querySelector(targetNode); 
            mo.observe(node , {
                childList: true
            });
        }", targetNode, idleCountNeeded, intervalDurationMilliseconds);

        await page.WaitForFunctionAsync(@"() => window.finished == true", new WaitForFunctionOptions() { Timeout = timeout });
        await page.EvaluateFunctionAsync(@"() => window.finished = false");
    }
}
