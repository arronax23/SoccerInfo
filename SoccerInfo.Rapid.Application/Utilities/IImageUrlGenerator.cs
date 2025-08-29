using Microsoft.AspNetCore.Http;

namespace SoccerInfo.Rapid.Application.Utilities;
public interface IImageUrlGenerator
{
    string Generate(HttpContext httpContext, Guid imageId, string imageType);
}
