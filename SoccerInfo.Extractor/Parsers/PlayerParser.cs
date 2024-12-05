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
        await Console.Out.WriteLineAsync(node.QuerySelector(".zentriert").InnerText);
        var (dateOfBirth, age) = ParseAgeAndDateOfBirth(node.QuerySelectorAll(".zentriert").ElementAt(1).InnerText);
        var (marketValue, marketValueUnit) = ParseMarketValue(node.QuerySelector(".rechts.hauptlink").InnerText);

        var player = new PlayerDto()
        {
            Name = node.QuerySelector(".hauptlink").InnerText.FormatExtractedStrings(),
            Number = ParseNumber(node.QuerySelector(".rueckennummer").InnerText.Trim()),
            Position = node.QuerySelectorAll("tr").Last().InnerText.FormatExtractedStrings(),
            Age = age,
            DateOfBirth = dateOfBirth,
            MarketValue = marketValue,
            MarketValueUnit = marketValueUnit,
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


    private int? ParseNumber(string inputNumber)
    {
        return int.TryParse(inputNumber, out int number) ? number : null;
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
            var tab = marketValueText.Split(' ');

            var marketValue = float.Parse(tab[0]);
            var marketValueUnit = tab[1];

            return (marketValue, marketValueUnit);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{nameof(ParseMarketValue)} exception");
            Console.WriteLine(ex.ToString());

            return (null, null);
        }
    }
}
