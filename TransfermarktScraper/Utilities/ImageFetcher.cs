using Microsoft.Extensions.Logging;

namespace TransfermarktScraper.Utilities;
public class ImageFetcher(
    ILogger<ImageFetcher> logger,
    IHttpClientFactory httpClientFactory)
{
    public async Task<string?> Fetch(string? imageUrl)
    {
        if (imageUrl == null)
            return null;

        using (var client = httpClientFactory.CreateClient())
        {
            try
            {
                var imageBinary = await client.GetByteArrayAsync(imageUrl);
                return Convert.ToBase64String(imageBinary);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}
