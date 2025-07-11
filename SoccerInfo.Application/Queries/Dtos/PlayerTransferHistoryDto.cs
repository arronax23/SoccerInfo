
namespace SoccerInfo.Application.Queries.Dtos;

public class PlayerTransferHistoryDto
{
    public IEnumerable<TransferDto>? Transfers { get; set; }

    public class TransferDto
    {
        public string ClubFromName { get; set; } = null!;
        public string? CLubFromBase64Image { get; set; } = null!;
        public string ClubToName { get; set; } = null!;
        public string? CLubToBase64Image { get; set; } = null!;
        public string TransferType { get; set; } = null!;
        public DateTime? Date { get; set; }
        public int? Age { get; set; }
        public string Season { get; set; } = null!;
        public MoneyDto? Fee { get; set; }
        public MoneyDto? MarketValue { get; set; }

        public class MoneyDto
        {
            private MoneyDto(string? value, string? suffix)
            {
                this.Value = value;
                this.Suffix = suffix;
            }
         
            public static MoneyDto Create(string? value, string? suffix) => new MoneyDto(value, suffix);

            public string? Value { get; set; }
            public string? Suffix { get; set; }
        }
    }

}
