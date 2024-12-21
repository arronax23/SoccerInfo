using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models;

public class Nationality : IEntity,  IEquatable<Nationality>
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string? Base64Image { get; set; }
    public ICollection<Player>? Players { get; set; }
    public CountryFlag_Lookup? CountryFlag { get; set; }
    public int? CountryFlagId { get; set; }

    public bool Equals(Nationality? other)
    {
        if (other == null)
            return false;
        else if (this.Country == other.Country)
            return true;
        else
            return false;
    }
}

