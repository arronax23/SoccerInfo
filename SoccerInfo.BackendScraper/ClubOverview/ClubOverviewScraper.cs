using Microsoft.Extensions.Logging;
using SoccerInfo.BackendScraper.Utilities;
using SoccerInfo.FrontendScraper.Utilities;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace SoccerInfo.BackendScraper.PlayersTransfers;
public class ClubOverviewScraper(
    ILogger<ClubOverviewScraper> logger,
    IHttpClientFactory httpClientFactory, 
    ImageFetcher imageFetcher)
{
    public async Task<IEnumerable<ClubOverviewData>> Scrape(IEnumerable<int> clubsTransfermarktIds)
    {
        using var client = httpClientFactory.CreateClient();
        client.AddHeadersForScrape();

        ConcurrentBag<ClubOverviewData> collectionData = new();

        await Parallel.ForEachAsync(
            clubsTransfermarktIds,
            new ParallelOptions { MaxDegreeOfParallelism = 50 },
            async (clubId, _) =>
            {
                try
                {
                    var clubModel = await client.GetFromJsonAsync<ClubModel>($"https://tmapi-alpha.transfermarkt.technology/club/{clubId}");
                    var data = await Map(clubModel!, clubId);

                    if (data is not null)
                        collectionData.Add(data);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"ClubOverviewScraper failed to fetch club transfermarkt Id = {clubId}");
                    logger.LogWarning(ex.ToString());
                }

            });

        return collectionData;
    }


    private async Task<ClubOverviewData> Map(ClubModel clubModel, int transfermarktId)
    {
        return new ClubOverviewData
        {
            TransfermarktId = transfermarktId,
            TransfermarktURL = clubModel.Data.RelativeUrl,
            Name = clubModel.Data.Name,
            Base64Image = await imageFetcher.Fetch(clubModel.Data.CrestUrl)
        };
    }


    public class ClubOverviewData
    {
        public int TransfermarktId { get; set; }
        public string Name { get; set; } = null!;
        public string? Base64Image { get; set; } = null!;
        public string TransfermarktURL { get; set; } = null!;
    }


    public class ClubModel
    {
        public DataModel Data { get; set; } = null!;
    }

    public class DataModel
    {
        public string Name { get; set; } = null!;
        public string CrestUrl { get; set; } = null!;
        public string RelativeUrl { get; set; } = null!;
    }

}
