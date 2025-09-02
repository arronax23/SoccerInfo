namespace SoccerInfo.FrontendScraper.Utilities;
public class MimeTypeService
{
    public string DetermineMimeTypeByImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new ArgumentException("Image URL cannot be null or empty", nameof(imageUrl));

        imageUrl = imageUrl.ToLowerInvariant();

        if (imageUrl.Contains(".jpg") || imageUrl.Contains(".jpeg"))
            return "image/jpeg";

        if (imageUrl.Contains(".png"))
            return "image/png";

        if (imageUrl.Contains(".gif"))
            return "image/gif";

        if (imageUrl.Contains(".webp"))
            return "image/webp";

        if (imageUrl.Contains(".svg"))
            return "image/svg+xml";

        throw new ArgumentException($"Image URL: {imageUrl} has not known mime Type");
    }
}