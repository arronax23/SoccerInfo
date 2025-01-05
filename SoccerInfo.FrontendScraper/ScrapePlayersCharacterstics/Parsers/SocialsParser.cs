using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Parsers;

public class SocialsParser
{
    public void Parse(HtmlNode socialMediaIconsNode, PlayerCharacteristicsData playerCharacteristics)
    {
        if (socialMediaIconsNode != null && socialMediaIconsNode.ChildNodes.Count > 0) 
        {
            foreach (var socialMediaItem in socialMediaIconsNode.QuerySelectorAll("a"))
            {
                playerCharacteristics.Socials.Add(new SocialMediaData()
                {
                    Link = socialMediaItem.ExtractAttribute("href"),
                    Platform = socialMediaItem.ExtractAttribute("title")
                });
            }
        }
    }
}
