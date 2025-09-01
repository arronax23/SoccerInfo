using SoccerInfo.Domain.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Models;
public class League : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual Image? Logo { get; set; } = new Image();
    public int? LogoId { get; set; }
    public string Country { get; set; } = null!;
    public virtual Image? CountryFlag { get; set; }
    public int? CountryFlagId { get; set; }
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public static Expression<Func<League, bool>> Matches(League other) =>
        (League l) => l.Name == other.Name && l.Country == other.Country;

    public void Update(League league)
    {
        this.Name = league.Name;
        this.Logo = league.Logo;
        this.Country = league.Country;
        this.CountryFlag = league.CountryFlag;
    }
}
