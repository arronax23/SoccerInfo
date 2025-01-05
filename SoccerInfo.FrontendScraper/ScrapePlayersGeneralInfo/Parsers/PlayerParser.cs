using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using SoccerInfo.FrontendScraper.Utilities;
using System.Globalization;
using System.Text.RegularExpressions;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
public class PlayerParser(ImageFetcher imageFetcher)
{
    public async Task<PlayerData> Parse(HtmlNode node)
    {
        await Console.Out.WriteLineAsync(node.QuerySelector(".hauptlink").InnerText.FormatExtractedString());
        var (dateOfBirth, age) = ParseAgeAndDateOfBirth(node.QuerySelectorAll(".zentriert").ElementAt(1).InnerText);
        var (marketValue, marketValueUnit) = ParseMarketValue(node.QuerySelector(".rechts.hauptlink").InnerText);
        var (transfermarktId, transfermarktURL) = ParseTransfermarktNavigationData(
            node.QuerySelector(".hauptlink a").GetAttributeValue("href", "notFound"));

        var player = new PlayerData()
        {
            TransfermarktId = transfermarktId,
            TransfermarktURL = transfermarktURL,
            Name = node.QuerySelector(".hauptlink").InnerText.FormatExtractedString(),
            Number = ParseNumber(node.QuerySelector(".rueckennummer").InnerText.Trim()),
            Position = node.QuerySelectorAll("tr").Last().InnerText.FormatExtractedString(),
            Age = age,
            DateOfBirth = dateOfBirth,
            MarketValue = marketValue,
            MarketValueUnit = marketValueUnit,
            FaceImageBase64 = await imageFetcher.Fetch(
                node.QuerySelector("img.bilderrahmen-fixed").GetAttributeValue("data-src", "notFound"))
        };

        return player;
    }

    private int? ParseNumber(string inputNumber)
    {
        return int.TryParse(inputNumber, out int number) ? number : null;
    }


    private (int, string) ParseTransfermarktNavigationData(string transfermarktURL)
    {
        var transfermarktId = int.Parse(transfermarktURL.Split('/').Last());

        return (transfermarktId, transfermarktURL);

    }
    private (DateTime, int) ParseAgeAndDateOfBirth(string ageAndDateOfBirth)
    {
        var tab = ageAndDateOfBirth.Split('(', ')');

        var dateOfBirth = DateTime.Parse(tab[0]);
        var age = int.Parse(tab[1]);

        return (dateOfBirth, age);
    }

    private (float?, string?) ParseMarketValue(string marketValueText)
    {
        try
        {

            marketValueText = marketValueText.Replace("€", "").FormatExtractedString().Trim();

            var match = Regex.Match(marketValueText, @"[\d.]+|[^\d\s.]+");

            if (match.Success)
            {
                float marketValue = float.Parse(match.Value, CultureInfo.InvariantCulture);
                string marketValueUnit = marketValueText.Trim().Substring(match.Value.Length).Trim();

                return (marketValue, marketValueUnit);
            }
            else
            {
                Console.WriteLine($"Could not Extract from {marketValueText}");
                return (null, null);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(ParseMarketValue)} exception");
            Console.WriteLine(ex.ToString());

            return (null, null);
        }
    }
}
