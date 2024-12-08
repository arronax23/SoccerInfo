using SoccerInfo.Persistence.Data.Models.Abstractions;

namespace SoccerInfo.Persistence.Data.Models;

public class NationalityImage : IEntity,  IEquatable<NationalityImage>
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string? Base64Image { get; set; }
    public ICollection<Player>? Players { get; set; }

    public bool Equals(NationalityImage? other)
    {
        if (other == null)
            return false;
        else if (this.Country == other.Country)
            return true;
        else
            return false;
    }
}

