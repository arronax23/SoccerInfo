namespace SoccerInfo.Persistence.Data.Models;


public class Player : IEquatable<Player>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int Age { get; set; }
    public float? MarketValue { get; set; }
    public string? MarketValueUnit { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? FaceImageBase64 { get; set; }
    public ICollection<NationalityImage>? NationalityImages { get; set; }
    public int TeamId { get; set; }


    public bool Equals(Player? other)
    {
        if (other == null) 
            return false;
        else if (this.Name == other.Name)
            return true;
        else
            return false;
    }
}
