using SoccerInfo.Persistence.Data.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Data.Models;

public class MarketValueChange : IEntity, IEquatable<MarketValueChange>
{
    public int Id { get; set; }
    public int PlayerTransferMarktId { get; set; }
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; } = string.Empty;
    public float? MarketValueNormalized { get; set; }
    public DateTime ChangeDate { get; set; }
    public string Team { get; set; } = string.Empty;
    public int PlayerId { get; set; }

    [Obsolete]
    public bool Equals(MarketValueChange? other)
    {
        if (other == null)
            return false;
        else if (this.PlayerTransferMarktId == other.PlayerTransferMarktId && this.ChangeDate == other.ChangeDate)
            return true;
        else
            return false;
    }

    public static Func<MarketValueChange, bool> Matches(MarketValueChange other) =>
        (MarketValueChange mvc) =>
            mvc.PlayerTransferMarktId == other.PlayerTransferMarktId &&
            mvc.ChangeDate == other.ChangeDate;
}
