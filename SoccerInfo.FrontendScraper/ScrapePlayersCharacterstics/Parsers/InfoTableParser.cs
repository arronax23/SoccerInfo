using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;

public class InfoTableParser
{
    public void Parse(HtmlNode infoTableNode, PlayerCharacteristicsData playerCharacteristics)
    {
        playerCharacteristics.Height = ParseHeight(infoTableNode);

        playerCharacteristics.LeadingFoot =
            NavigateToDataNode(infoTableNode, "Foot")?
            .InnerText.FormatExtractedString();


        playerCharacteristics.ClubJoinDate =
             NavigateToDataNode(infoTableNode, "Joined")?
            .InnerText.FormatExtractedString()
            .TryParseToDate();

        playerCharacteristics.ContractExpirationDate =
             NavigateToDataNode(infoTableNode, "Contract expires")?
            .InnerText.FormatExtractedString()
            .TryParseToDate();

        playerCharacteristics.BrithPlace = 
            ParseBrithPlace(
                NavigateToDataNode(infoTableNode, "Place of birth"));
    }


    private float? ParseHeight(HtmlNode infoTableNode)
    {
        return NavigateToDataNode(infoTableNode, "Height")?
            .InnerText.FormatExtractedString().Replace("m", "").Trim()
            .TryParseToFloat();
    }


    private BrithPlaceData? ParseBrithPlace(HtmlNode? birthPlaceNode)
    {
        if (birthPlaceNode == null) 
            return null;    

        return new BrithPlaceData()
        { 
            City = birthPlaceNode.InnerText.FormatExtractedString(),
            Country = birthPlaceNode.QuerySelector("img").ExtractAttribute("title")
        };
    }

    private HtmlNode? NavigateToDataNode(HtmlNode infoTableNode, string labelStartsWith)
    {
        try
        {
            return infoTableNode
            .ChildNodes
            .Single(x => x.InnerText.FormatExtractedString().StartsWith(labelStartsWith))
            .NextSiblingElement();
        }
        catch (Exception)
        {
            return null;
        }
    }

}
