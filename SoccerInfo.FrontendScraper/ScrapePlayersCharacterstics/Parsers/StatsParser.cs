using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;

public class StatsParser(ImageFetcher imageFetcher, ILogger<StatsParser> logger)
{
    public async Task Parse(HtmlNode gridTableNode, PlayerCharacteristicsData playerCharacteristics)
    {
        gridTableNode.GetChildElementNodes().First().Remove();
        gridTableNode.GetChildElementNodes().Last().Remove();

        if (playerCharacteristics.IsGoalkeeper)
            foreach (var leagueNode in gridTableNode.GetChildElementNodes())
                playerCharacteristics.GoalKeeperStats!.Add(await ParseGoalKeeper(leagueNode));
        else
            foreach (var leagueNode in gridTableNode.GetChildElementNodes())
                playerCharacteristics.OutfieldPlayerStats!.Add(await ParseOutFieldPlayer(leagueNode));
    }


    private async Task<GoalKeeperStatsData> ParseGoalKeeper(HtmlNode leagueNode)
    {
        return new GoalKeeperStatsData()
        {
            League = leagueNode.GetChildElementNodes().ElementAt(0).QuerySelector("a").InnerText.FormatExtractedString(),
            LeagueBase64Image = await FetchLeagueImage(leagueNode),
            MatchesPlayed = leagueNode.GetChildElementNodes().ElementAt(1).QuerySelector("a").InnerText.FormatExtractedString().TryParseToInt(),
            GoalsConceded = leagueNode.GetChildElementNodes().ElementAt(2).InnerText.FormatExtractedString().TryParseToInt(),
            CleanSheets = leagueNode.GetChildElementNodes().ElementAt(3).InnerText.FormatExtractedString().TryParseToInt(),
            MinutesPlayed = leagueNode.GetChildElementNodes().ElementAt(4).FirstChild.InnerText.FormatExtractedString().Replace("\'", "").Replace(",", "").TryParseToInt(),
        };
    }



    private async Task<OutfieldPlayerStatsData> ParseOutFieldPlayer(HtmlNode leagueNode)
    {
        return new OutfieldPlayerStatsData()
        {
            League = leagueNode.GetChildElementNodes().ElementAt(0).QuerySelector("a").InnerText.FormatExtractedString(),
            LeagueBase64Image = await FetchLeagueImage(leagueNode),
            MatchesPlayed = leagueNode.GetChildElementNodes().ElementAt(1).QuerySelector("a").InnerText.FormatExtractedString().TryParseToInt(),
            Goals = leagueNode.GetChildElementNodes().ElementAt(2).InnerText.FormatExtractedString().TryParseToInt(),
            Assists = leagueNode.GetChildElementNodes().ElementAt(3).InnerText.FormatExtractedString().TryParseToInt(),
            MinutesPlayed = leagueNode.GetChildElementNodes().ElementAt(5).InnerText.FormatExtractedString().Replace("\'", "").Replace(",", "").TryParseToInt(),
        };
    }

    private async Task<string?> FetchLeagueImage(HtmlNode leagueNode)
    {
        var imageSouce = leagueNode
            .GetChildElementNodes()
            .ElementAt(0)
            .QuerySelector("img")
            .ExtractAttribute("src") ?? string.Empty;

        if (imageSouce.StartsWith("http"))
            return await imageFetcher.Fetch(imageSouce);
        else if (imageSouce.StartsWith("data:image/svg+xml"))
            return imageSouce.Split(',').ElementAt(1).Trim();
        else
            return null;
    }
}
