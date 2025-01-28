using Azure;
using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;

public class StatsLeague : IEntity, IEquatable<StatsLeague>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Base64Image { get; set; }
    public ICollection<GoalKeeperStats>? GoalKeeperStats { get; set; }
    public ICollection<OutfieldPlayerStats>? OutfieldPlayerStats { get; set; }

    private int _hashCode; 

    public bool Equals(StatsLeague? other)
    {
        if (other == null)
            return false;
        else if (this.Name == other.Name && this.Base64Image == other.Base64Image)
            return true;
        else 
            return false;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StatsLeague);    
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Base64Image);
    }
}