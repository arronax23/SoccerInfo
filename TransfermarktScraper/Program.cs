using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using TransfermarktScraper;

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var logger = loggerFactory.CreateLogger<PuppeteerManager>();

var BASE_URI = "https://www.transfermarkt.pl";

var puppeteerManager = new PuppeteerManager(logger);

var page = await puppeteerManager.InitializePage(headless: false);
await page.GoToAsync("https://www.transfermarkt.pl/premier-league/startseite/wettbewerb/GB1", WaitUntilNavigation.Networkidle0);
var n = await page.CreateHtmlNodeFromPage();
var teams =  n.QuerySelector("table.items").QuerySelectorAll(".hauptlink a[title]");
var teamsLinks = teams.Select(x => BASE_URI + x.GetAttributeValue("href", "Not found"));

foreach (var link in teamsLinks)
{
    await page.GoToAsync($"{link}");
    await page.WaitForNetworkIdleAsync();
    var node = await page.CreateHtmlNodeFromPage();
    await GetPlayers(node, link);
}

await puppeteerManager.CloseBrowser();



static async Task GetPlayers(HtmlNode node, string link)
{
    var tableNode = node.QuerySelector("table.items");
    var teamName = node.QuerySelector(".data-header__headline-container").InnerText;

    Traverser traverser = new Traverser();
    traverser.DFS(tableNode, EndSelectorsSpecification, SelectorsSpecification);
    var results = traverser.FoundNodes;

    await Console.Out.WriteLineAsync(teamName);
    foreach (var item in results)
    {
        await Console.Out.WriteLineAsync(item.QuerySelector(".hauptlink").InnerText.FormatExtractedStrings());
    }
}

static bool EndSelectorsSpecification(HtmlNode node)
{
    return node.HasClass("odd") || node.HasClass("even");
}

static bool SelectorsSpecification(HtmlNode node)
{
    return false;
}