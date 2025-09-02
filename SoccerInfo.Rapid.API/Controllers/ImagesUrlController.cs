using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class ImagesUrlController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("images/{imageId}")]
    public async Task<IActionResult> GetLeagueImage(int imageId)
    {
        var imageDto = await queryDispatcher.Send(new GetLeagueImageQuery() { ImageId = imageId });

        if( imageDto is not null)
        {
            var imageBytes = Convert.FromBase64String(imageDto.Base64);
            return File(imageBytes, imageDto.MimeType);
        }
        else
            return NotFound();
    }
}
