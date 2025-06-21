using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Models;

public class MarketValueChange : BaseEntity
{
    public int PlayerTransferMarktId { get; set; }
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; } = string.Empty;
    public float? MarketValueNormalized { get; set; }
    public DateTime ChangeDate { get; set; }
    public string Team { get; set; } = string.Empty;
    public int PlayerId { get; set; }

    public static Func<MarketValueChange, bool> Matches(MarketValueChange other) =>
        (MarketValueChange mvc) =>
            mvc.PlayerTransferMarktId == other.PlayerTransferMarktId &&
            mvc.ChangeDate == other.ChangeDate;
}
