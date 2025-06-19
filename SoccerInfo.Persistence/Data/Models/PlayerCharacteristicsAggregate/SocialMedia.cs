using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

public class SocialMedia : BaseEntity, IEntity, IEquatable<SocialMedia>
{
    public string? Platform { get; set; }
    public string? Link { get; set; }
    public int PlayerCharacteristicId { get; set; }

    public bool Equals(SocialMedia? other)
    {
        if (other == null)
            return false;
        else if (this.Platform == other.Platform)
            return true;
        else 
            return false;
    }
}