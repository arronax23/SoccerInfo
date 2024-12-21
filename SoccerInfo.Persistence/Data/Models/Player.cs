using SoccerInfo.Persistence.Data.Models.Abstractions;
namespace SoccerInfo.Persistence.Data.Models;

public class Player : IEntity, IEquatable<Player>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? FaceImageBase64 { get; set; }
    public ICollection<Nationality>? NationalityImages { get; set; }
    public int TransfermarktId { get; set; }
    public string TransfermarktURL { get; set; } = null!;
    public int TeamId { get; set; }

    public bool Equals(Player? other)
    {
        if (other == null) 
            return false;
        else if (this.Name == other.Name && this.DateOfBirth == other.DateOfBirth)
            return true;
        else
            return false;
    }
}
