using SoccerInfo.Domain.Models.Abstractions;

namespace SoccerInfo.Domain.Models;

public class Image : BaseEntity
{
    public string Base64 { get; set; } = null!;
    public string MimeType { get; set; } = null!;

    public void Update(Image image)
    {
        this.Base64 = image.Base64;
        this.MimeType = image.MimeType;
    }
}
