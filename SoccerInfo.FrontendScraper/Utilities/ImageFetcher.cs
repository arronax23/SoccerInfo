using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace SoccerInfo.FrontendScraper.Utilities;
public class ImageFetcher(
    ILogger<ImageFetcher> logger,
    IHttpClientFactory httpClientFactory)
{
    public async Task<string?> Fetch(string? imageUrl)
    {
        if (imageUrl == null || imageUrl == "notFound")
            return null;

        using (var client = httpClientFactory.CreateClient())
        {
            try
            {
                var imageBinary = await client.GetByteArrayAsync(imageUrl);
                return Regex.Unescape(Convert.ToBase64String(imageBinary));
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}
