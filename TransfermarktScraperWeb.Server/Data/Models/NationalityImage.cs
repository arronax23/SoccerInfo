namespace SoccerInfo.Infrastructure.Data.Models;

public class NationalityImage
{
    public int NationalityImageId { get; set; }
    public string? Base64Image { get; set; }
    public int PlayerId { get; set; }
}
