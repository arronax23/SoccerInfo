using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;

namespace SoccerInfo.Extractor.Parsers;
public class PlayerParser(
    ImageFetcher imageFetcher)
{
    public async Task<PlayerDto> Parse(HtmlNode node)
    {
        var player = new PlayerDto()
        {
            Name = node.QuerySelector(".hauptlink").InnerText.FormatExtractedStrings(),
            Position = node.QuerySelectorAll("tr").Last().InnerText.FormatExtractedStrings(),
            FaceImageBase64 = await imageFetcher.Fetch(
                node.QuerySelector("img.bilderrahmen-fixed").GetAttributeValue("data-src", "notFound"))
        };

        foreach (var flag in node.QuerySelectorAll("img.flaggenrahmen"))
        {
            player.NationalityImageBase64Collection.Add(
                await imageFetcher.Fetch(flag.GetAttributeValue("src", "notFound")));
        }

        return player;
    }
}
