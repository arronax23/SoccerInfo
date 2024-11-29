namespace TransfermarktScraperWeb.Server.Data.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string? FaceImageBase64 { get; set; }
    public ICollection<NationalityImage>? NationalityImageBase64Collection { get; set; }
    public int TeamId { get; set; }

}
