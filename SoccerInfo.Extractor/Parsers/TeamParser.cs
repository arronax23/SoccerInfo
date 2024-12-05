using HtmlAgilityPack;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;

namespace SoccerInfo.Extractor.Parsers;
internal class TeamParser
{
    public void Parse(HtmlNode playerNode)
    {
        //var player = new PlayerDto()
        //{
        //    PlayerName = playerNode.QuerySelector(".hauptlink").InnerText.FormatExtractedStrings(),
        //    Position = playerNode.QuerySelectorAll("tr").Last().InnerText.FormatExtractedStrings(),
        //    FaceImageBase64 = await imageFetcher.Fetch(
        //        playerNode.QuerySelector("img.bilderrahmen-fixed").GetAttributeValue("data-src", "notFound"))
        //};

        //foreach (var flag in playerNode.QuerySelectorAll("img.flaggenrahmen"))
        //{
        //    player.NationalityImageBase64Collection.Add(
        //        await imageFetcher.Fetch(flag.GetAttributeValue("src", "notFound")));
        //}

        //return player;
    }
}
