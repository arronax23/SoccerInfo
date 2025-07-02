using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
public class TeamParser(
    ImageFetcher imageFetcher)
{
    public async Task<TeamData> Parse(HtmlNode node)
    {
        var teamImageSource = node.QuerySelector(".data-header__profile-container img").GetAttributeValue("src", "notFound");

        return new TeamData()
        {
            Name = node.QuerySelector(".data-header__headline-container").InnerText.FormatExtractedString(),
            TransfermarktId = int.Parse(teamImageSource.Split("head/")[1].Split('.')[0]),
            TeamImageBase64 = await imageFetcher.Fetch(teamImageSource)
        };
    }
}


