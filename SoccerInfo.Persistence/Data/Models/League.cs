using SoccerInfo.Persistence.Data.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Data.Models;
public class League : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? LeagueImageBase64 { get; set; }
    public string Country { get; set; } = null!;
    public string? CountryFlagBase64 { get; set; }
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public static Expression<Func<League, bool>> Matches(League other) =>
        (League l) => l.Name == other.Name && l.Country == other.Country;

    public void Update(League league)
    {
        this.Name = league.Name;
        this.LeagueImageBase64 = league.LeagueImageBase64;
        this.Country = league.Country;
        this.CountryFlagBase64 = league.CountryFlagBase64;
    }


}
