using HtmlAgilityPack;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionData;

namespace SoccerInfo.Extractor.Parsers;
public class NationalityParser(ImageFetcher imageFetcher)
{
    public async Task<NationalityData> Parse(HtmlNode node)
    {
        return new NationalityData()
        {
            Country = node.GetAttributeValue("title", "notFound"),
            Base64Image = await imageFetcher.Fetch(node.GetAttributeValue("src", "notFound"))
        };
    }
}
