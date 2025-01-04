using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;


namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
public class LeagueParser(
    ImageFetcher imageFetcher)
{
    public async Task<LeagueData> Parse(HtmlNode node)
    {
        return new LeagueData()
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
