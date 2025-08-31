using Microsoft.AspNetCore.Http;
using SoccerInfo.Rapid.Application.Utilities;

namespace SoccerInfo.Rapid.API;
public class ImageUrlGenerator(IHttpContextAccessor httpContextAccessor) : IImageUrlGenerator
{
    public string Generate(int imageId)
    {
        var request = httpContextAccessor.HttpContext.Request;

        return $"{request.Scheme}://{request.Host}/{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}/Images/{imageId}";
    }
}
