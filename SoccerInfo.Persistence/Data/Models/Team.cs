using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models;

public class Team : IEntity,  IEquatable<Team>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamImageBase64 { get; set; }
    public virtual ICollection<Player>? Players { get; set; }
    public int? LeagueId { get; set; }
    public virtual League? League { get; set; }

    public bool Equals(Team? other)
    {
        if (other == null)
            return false;
        else if (this.Name == other.Name)
            return true;
        else
            return false;
    }
}
