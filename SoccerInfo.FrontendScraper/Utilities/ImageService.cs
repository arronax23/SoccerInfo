using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;

namespace SoccerInfo.FrontendScraper.Utilities;
public class ImageService(
    ImageFetcher imageFetcher,
    MimeTypeService mimeTypeService)
{
    public async Task<ImageData?> CreateImageFromUrl(string? faceImageUrl)
    {
        if (faceImageUrl is null)
            return null;

        var base64 = await imageFetcher.Fetch(faceImageUrl);
        var mimeType = mimeTypeService.DetermineMimeTypeByImageUrl(faceImageUrl);

        return new ImageData() { Base64 = base64, MimeType = mimeType };
    }
}
