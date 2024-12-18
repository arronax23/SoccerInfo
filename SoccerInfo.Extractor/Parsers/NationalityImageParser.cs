using HtmlAgilityPack;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionData;

namespace SoccerInfo.Extractor.Parsers;
public class NationalityImageParser(ImageFetcher imageFetcher)
{
    public async Task<NationalityImageData> Parse(HtmlNode node)
    {
        return new NationalityImageData()
        {
            Country = node.GetAttributeValue("title", "notFound"),
            Base64Image = await imageFetcher.Fetch(node.GetAttributeValue("src", "notFound"))
        };
    }
}
