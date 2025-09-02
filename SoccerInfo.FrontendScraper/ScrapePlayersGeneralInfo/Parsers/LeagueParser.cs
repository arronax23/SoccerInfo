using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
public class LeagueParser(
    ImageService imageService)
{
    public async Task<LeagueData> Parse(HtmlNode node)
    {
        return new LeagueData()
        {
            Name = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedString(),
            Country = node.QuerySelector(".data-header__club").InnerText.FormatExtractedString(),
            CountryFlag = await imageService.CreateImageFromUrl(
                node.QuerySelector(".data-header__box__club-link img").ExtractAttribute("src")),
            Logo = await imageService.CreateImageFromUrl(
                node.QuerySelector(".data-header__profile-container img").ExtractAttribute("src"))
        };
    }
}
