namespace SoccerInfo.Persistence.Data.Models;

public class NationalityImage : IEquatable<NationalityImage>
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string? Base64Image { get; set; }
    public int PlayerId { get; set; }

    public bool Equals(NationalityImage? other)
    {
        if (other == null)
            return false;
        else if (this.Base64Image == other.Base64Image)
            return true;
        else
            return false;
    }
}

