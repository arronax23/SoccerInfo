using Microsoft.AspNetCore.Http;

namespace SoccerInfo.Rapid.Application.Utilities;
public interface IImageUrlGenerator
{
    string Generate(int imageId);
}
