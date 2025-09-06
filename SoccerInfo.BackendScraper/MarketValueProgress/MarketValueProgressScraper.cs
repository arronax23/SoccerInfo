using System.Globalization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

namespace SoccerInfo.BackendScraper.MarketValueProgress;

public class MarketValueProgressScraper(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<MarketValueProgressData>> Scrape(IEnumerable<int> playersTransfermarktIds)
    {
        using var client = httpClientFactory.CreateClient();

        List<MarketValueProgressData> collectionData = new();

        await Parallel.ForEachAsync(
            playersTransfermarktIds,
            new ParallelOptions { MaxDegreeOfParallelism = 50 },
            async (playerId, _) =>
        {
            MarketValueProgressData progressData = await client.GetFromJsonAsync<MarketValueProgressModel>($"https://www.transfermarkt.com/ceapi/marketValueDevelopment/graph/{playerId}");
            progressData.PlayerTransfermarktId = playerId;
            collectionData.Add(progressData);
        });

        return collectionData;
    }

    public class MarketValueProgressData
    {
        public int PlayerTransfermarktId { get; set; }
        public IEnumerable<ProgressItem>? ProgressCollection { get; set; }

        public class ProgressItem
        {
            public float? MarketValue { get; set; }
            public string? MarketValueUnit { get; set; } = string.Empty;
            public DateTime ChangeDate { get; set; }
            public string Team { get; set; } = string.Empty;
            public int Age { get; set; }
        }

        private static (float?, string?) ParseMarketValue(string mw)
        {
            if (mw == "-")
                return (null, null);

            mw = mw.Replace("€","").Trim();
            var match = Regex.Match(mw, @"[\d.]+|[^\d\s.]+");

            if (match.Success)
            {
                float marketValue = float.Parse(match.Value, CultureInfo.InvariantCulture);
                string marketValueUnit = mw.Trim().Substring(match.Value.Length).Trim();

                return (marketValue, marketValueUnit);
            }
            else
                throw new Exception("Failed parsing MarketValue");
        }


        public static implicit operator MarketValueProgressData(MarketValueProgressModel? input)
        {
            return new MarketValueProgressData()
            {
                ProgressCollection = input.List.Select(x => new ProgressItem()
                {
                    MarketValue = ParseMarketValue(x.Mw).Item1,
                    MarketValueUnit = ParseMarketValue(x.Mw).Item2,
                    Age = x.Age,
                    ChangeDate = DateTime.Parse(x.Datum_mw),
                    Team = x.Verein
                })
            };
        }
    }

    public class MarketValueProgressModel
    {
        public IEnumerable<ListModel> List { get; set; }
        public class ListModel
        {
            public string Mw { get; set; }
            public string Datum_mw { get; set; }
            public string Verein { get; set; }
            public int Age { get; set; }

        }
    }
}