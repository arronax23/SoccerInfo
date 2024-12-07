namespace SoccerInfo.Persistence.Data.Models;

public class Team : IEquatable<Team>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? TeamImageBase64 { get; set; }
    public ICollection<Player>? Players { get; set; }
    public int? LeagueId { get; set; }
    public League? League { get; set; }

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
