using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.Dto.ExtractionData;

namespace SoccerInfo.FrontendScraper.Parsers;
public class TeamParser(
    ImageFetcher imageFetcher)
{
    public async Task<TeamData> Parse(HtmlNode node)
    {
        return new TeamData()
        {
            Name = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedStrings(),
            TeamImageBase64 = await imageFetcher.Fetch(
                node.QuerySelector(".data-header__profile-container img").GetAttributeValue("src", "notFound"))
        };
    }
}
    

