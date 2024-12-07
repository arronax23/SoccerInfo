namespace SoccerInfo.Persistence.Data.Models;
public class League : IEquatable<League>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LeagueImageBase64 { get; set; }
    public string Country { get; set; } = null!;
    public string? CountryFlagBase64 { get; set; }
    public ICollection<Team>? Teams { get; set; }

    public bool Equals(League? other)
    {
        if (other == null)
            return false;
        else if (this.Name == other.Name && this.Country == other.Country)
            return true;
        else
            return false;
    }


}
