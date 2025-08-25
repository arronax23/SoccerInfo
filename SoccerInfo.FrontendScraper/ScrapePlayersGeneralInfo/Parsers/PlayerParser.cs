using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using SoccerInfo.FrontendScraper.Utilities;
using System.Globalization;
using System.Text.RegularExpressions;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Parsers;
public class PlayerParser(
    ILogger<PlayerParser> logger,
    ImageFetcher imageFetcher)
{
    public async Task<PlayerData> Parse(HtmlNode node)
    {
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
    private (DateTime?, int?) ParseAgeAndDateOfBirth(string ageAndDateOfBirth)
    {
        try
        {
            var tab = ageAndDateOfBirth.Split('(', ')');
            var dateOfBirthParseSuccess = DateTime.TryParseExact(
                tab[0].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateOfBirth);
            var ageParseSuccess = int.TryParse(tab[1].Trim(), out var age);

            DateTime? dateOfBirthNullable = dateOfBirthParseSuccess ? dateOfBirth : null;
            int? ageNullable = ageParseSuccess ? age : null;

            return (dateOfBirthNullable, ageNullable);
        }
        catch (Exception ex)
        {
            logger.LogError($"ageAndDateOfBirth: {ageAndDateOfBirth}");
            logger.LogError($"Line 69");
            logger.LogError(ex.ToString());
            throw;
        }
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
                logger.LogWarning($"Could not Extract from {marketValueText}");
                return (null, null);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning($"{nameof(ParseMarketValue)} exception");
            logger.LogWarning(ex.ToString());

            return (null, null);
        }
    }
}
