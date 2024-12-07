using HtmlAgilityPack;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;

namespace SoccerInfo.Extractor.Parsers;
public class NationalityImageParser(ImageFetcher imageFetcher)
{
    public async Task<NationalityImageDto> Parse(HtmlNode node)
    {
        return new NationalityImageDto()
        {
            Country = node.GetAttributeValue("title", "notFound"),
            Base64Image = await imageFetcher.Fetch(node.GetAttributeValue("src", "notFound"))
        };
    }
}
