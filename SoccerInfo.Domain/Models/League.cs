using SoccerInfo.Domain.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Models;
public class League : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual Image? Logo { get; set; }
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
        this.Country = league.Country;

        UpdateLogo(league.Logo);
        UpdateCountryFlag(league.CountryFlag);
    }


    private void UpdateLogo(Image? logo)
    {
        if (logo != null)
        {
            if (this.Logo != null)
                this.Logo.Update(logo);
            else
                this.Logo = logo;
        }
    }

    private void UpdateCountryFlag(Image? countryFlag)
    {
        if (countryFlag != null)
        {
            if (this.CountryFlag != null)
                this.CountryFlag.Update(countryFlag);
            else
                this.CountryFlag = countryFlag;
        }
    }
}
