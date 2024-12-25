using HtmlAgilityPack;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.Dto.ExtractionData;

namespace SoccerInfo.FrontendScraper.Parsers;
public class NationalityParser(ImageFetcher imageFetcher)
{
    public async Task<NationalityData> Parse(HtmlNode node)
    {
        return new NationalityData()
        {
            Country = node.GetAttributeValue("title", "notFound")
        };
    }
}
