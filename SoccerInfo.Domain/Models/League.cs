using SoccerInfo.Domain.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Models;
public class League : BaseEntity
{
    public string Name { get; set; } = null!;
    public Image? Logo { get; set; } = new Image();
    public string? LeagueImageBase64 { get; set; }
    public int? LogoId { get; set; }
    public string Country { get; set; } = null!;
    public string? CountryFlagBase64 { get; set; }
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public static Expression<Func<League, bool>> Matches(League other) =>
        (League l) => l.Name == other.Name && l.Country == other.Country;

    public void Update(League league)
    {
        this.Name = league.Name;
        this.Logo = league.Logo;
        this.Country = league.Country;
        this.CountryFlagBase64 = league.CountryFlagBase64;
    }
}
