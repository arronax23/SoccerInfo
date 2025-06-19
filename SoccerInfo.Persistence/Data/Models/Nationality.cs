using SoccerInfo.Persistence.Data.Models.Abstractions;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Data.Models;

public class Nationality : BaseEntity
{
    public string Country { get; set; } = null!;
    public string? Country_Lookup { get; set; }
    public virtual ICollection<Player>? Players { get; set; }
    public virtual CountryFlag_Lookup? CountryFlag { get; set; }
    public int? CountryFlagId { get; set; }

    public static Expression<Func<Nationality, bool>> Matches(Nationality other) =>
        (Nationality n) => n.Country == other.Country;

}

