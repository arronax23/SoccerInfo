using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models;

public class MarketValueChange : IEntity, IEquatable<MarketValueChange>
{
    public int Id { get; set; }
    public int PlayerTransferMarktId { get; set; }
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; } = string.Empty;
    public DateTime ChangeDate { get; set; }
    public string Team { get; set; } = string.Empty;
    public int PlayerId { get; set; }

    public bool Equals(MarketValueChange? other)
    {
        if (other == null)
            return false;
        else if (this.PlayerTransferMarktId == other.PlayerTransferMarktId && this.ChangeDate == other.ChangeDate)
            return true;
        else
            return false;
    }

}
