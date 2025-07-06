using System.Net.Http.Headers;
using System.Net.Http.Json;
using static SoccerInfo.BackendScraper.PlayersTransfers.PlayersTransfersScraper.PlayerTransfersData;
using static SoccerInfo.BackendScraper.PlayersTransfers.PlayersTransfersScraper.PlayerTransfersData.TransferDetailsData;

namespace SoccerInfo.BackendScraper.PlayersTransfers;
public class PlayersTransfersScraper(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<PlayerTransfersData>> Scrape(IEnumerable<int> playersTransfermarktIds)
    {
        using var client = httpClientFactory.CreateClient();
        AddClientHeaders(client);

        List<PlayerTransfersData> collectionData = new();

        await Parallel.ForEachAsync(
            playersTransfermarktIds,
            new ParallelOptions { MaxDegreeOfParallelism = 50 },
            async (playerId, _) =>
            {
                var responseModel = await client.GetFromJsonAsync<ResponseModel>($"https://tmapi-alpha.transfermarkt.technology/transfer/history/player/{playerId}");
                var data = MapResponseModelToData(responseModel);

                if (data is not null) 
                    collectionData.Add(data);
            });

        return collectionData;
    }


    private PlayerTransfersData? MapResponseModelToData(ResponseModel? responseModel)
    {
        if (responseModel is null)
            return null;

        return new PlayerTransfersData()
        {
            PlayerTransfermarktId = responseModel.Data.PlayerId,
            Transfers = responseModel.Data.History.Terminated.Select(x => new TransferItemData()
            {
                ClubFrom = new ClubData()
                {
                    TransfermarktId = x.TransferSource.ClubId
                },
                ClubTo = new ClubData()
                {
                    TransfermarktId = x.TransferDestination.ClubId
                },
                Details = new TransferDetailsData()
                {
                    Age = x.Details.Age,
                    Date = x.Details.Date,
                    Season = x.Details.Season.Display,
                    FeeNormalized = PrepareNormalizedAmount(x.Details.Fee?.Value),
                    MarketValueNormalized = PrepareNormalizedAmount(x.Details.MarketValue?.Value),
                    Fee = new MoneyData()
                    {
                        Value = x.Details.Fee?.Compact?.Content,
                        Suffix = x.Details.Fee?.Compact?.Suffix?.ToLower(),
                    },
                    MarketValue = new MoneyData()
                    {
                        Value = x.Details.MarketValue?.Compact?.Content,
                        Suffix = x.Details.MarketValue?.Compact?.Suffix?.ToLower(),
                    },
                    Type = x.TypeDetails.Type
                }
            })
        };
    }

    private void AddClientHeaders(HttpClient client)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private int? PrepareNormalizedAmount(int? value) 
    {
        return value == 0 ? null : value;  
    }

    public class PlayerTransfersData
    {
        public int PlayerTransfermarktId { get; set; }
        public IEnumerable<TransferItemData> Transfers { get; set; } = null!;

        public class TransferItemData
        {
            public ClubData ClubFrom { get; set; } = null!;
            public ClubData ClubTo { get; set; } = null!;
            public TransferDetailsData Details { get; set; } = null!;
        }

        public class TransferDetailsData
        {
            public string Season { get; set; } = null!;
            public MoneyData? MarketValue { get; set; }
            public int? MarketValueNormalized { get; set; }
            public MoneyData? Fee { get; set; }
            public int? FeeNormalized { get; set; }
            public int Age { get; set; }
            public string Date { get; set; } = null!;
            public string Type { get; set; } = null!;

            public class MoneyData
            {
                public string? Value { get; set; }
                public string? Suffix { get; set; }
            }
        }

        public class ClubData
        {
            public int TransfermarktId { get; set; }
        }
    }

    public class ResponseModel
    {
        public DataModel Data { get; set; } = null!;

        public class DataModel
        {
            public int PlayerId { get; set; }
            public HistoryModel History { get; set; } = null!;
        }

        public class HistoryModel
        {
            public IEnumerable<TerminatedModel> Terminated { get; set; } = null!;
        }

        public class TerminatedModel
        {
            public TransferModel TransferSource { get; set; } = null!;
            public TransferModel TransferDestination { get; set; } = null!;
            public DetailsModel Details { get; set; } = null!;
            public TypeDetailsModel TypeDetails { get; set; } = null!;
        }

        public class TransferModel
        {
            public int ClubId { get; set; }
        }

        public class DetailsModel
        {
            public string Date { get; set; } = null!;
            public string ContractUntilDate { get; set; } = null!;
            public int Age { get; set; }
            public SeasonModel Season { get; set; } = null!;
            public MarketValueModel? MarketValue { get; set; }
            public MarketValueModel? Fee { get; set; }


            public class SeasonModel
            {
                public string Display { get; set; } = null!;
            }

            public class MarketValueModel
            {
                public int? Value { get; set; }
                public string? Currency { get; set; } = null!;
                public CompactModel? Compact { get; set; } = null!;
            }
            public class CompactModel
            {
                public string? Content { get; set; }
                public string? Suffix { get; set; } = null!;
            }
        }

        public class TypeDetailsModel
        {
            public string Type { get; set; } = null!;
        }
    }
}
