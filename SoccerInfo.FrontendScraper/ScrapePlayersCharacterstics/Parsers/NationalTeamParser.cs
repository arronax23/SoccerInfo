using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;

public class NationalTeamParser
{
    public void Parse(HtmlNode nationalTeamNode, PlayerCharacteristicsData playerCharacteristics)
    {
        var capsAndGoalsNodes = nationalTeamNode
            .ChildNodes
            .Single(x => x.InnerText.FormatExtractedString().StartsWith("Caps/Goals"))
            .QuerySelectorAll("a");

        playerCharacteristics.NationalTeam = new NationalTeamData()
        {
            Caps = capsAndGoalsNodes.ElementAt(0).InnerText.FormatExtractedString().ParseToInt(),
            Goals = capsAndGoalsNodes.ElementAt(1).InnerText.FormatExtractedString().ParseToInt(),
            Country = nationalTeamNode.QuerySelector("img").ExtractAttribute("title")
        };
    }
}
