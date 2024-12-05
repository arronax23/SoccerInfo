using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.Extractor.Utilities;
using static SoccerInfo.Extractor.Dto.ExtractionDto;

namespace SoccerInfo.Extractor.Parsers;
public class LeagueParser(
    ImageFetcher imageFetcher)
{
    public async Task<LeagueDto> Parse(HtmlNode node)
    {
        return new LeagueDto()
        {
            Name = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedStrings(),
            Country = node.QuerySelector(".data-header__club").InnerText.FormatExtractedStrings(),
            CountryFlagBase64 = await imageFetcher.Fetch(
                node.QuerySelector(".data-header__box__club-link img").GetAttributeValue("src", "notFound")),
            LeagueImageBase64 = await imageFetcher.Fetch(
                node.QuerySelector(".data-header__profile-container img").GetAttributeValue("src", "notFound"))
        };
    }
}
