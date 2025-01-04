using HtmlAgilityPack;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
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
