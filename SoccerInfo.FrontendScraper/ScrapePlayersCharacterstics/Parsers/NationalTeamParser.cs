using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;

public class NationalTeamParser
{
    public void Parse(HtmlNode? nationalTeamNode, PlayerCharacteristicsData playerCharacteristics)
    {
        if (nationalTeamNode == null) 
            return;   

        string? name = null;

        try
        {
            name = nationalTeamNode
                .GetFirstChildElementNode()?
                .GetFirstChildElementNode()?
                .QuerySelector("a")?
                .ExtractAttribute("title");
        }
        catch (Exception)
        {
            return;
        }

        var capsAndGoalsNodes = nationalTeamNode
            .ChildNodes?
            .SingleOrDefault(x => x.InnerText.FormatExtractedString().StartsWith("Caps/Goals"))?
            .QuerySelectorAll("a");

        if (name is null|| capsAndGoalsNodes is null)
            return;

        playerCharacteristics.NationalTeam = new NationalTeamData()
        {
            Name = name,
            Caps = capsAndGoalsNodes.ElementAt(0).InnerText.FormatExtractedString().ParseToInt(),
            Goals = capsAndGoalsNodes.ElementAt(1).InnerText.FormatExtractedString().ParseToInt(),
            Country = nationalTeamNode.QuerySelector("img").ExtractAttribute("title")
        };
    }
}
