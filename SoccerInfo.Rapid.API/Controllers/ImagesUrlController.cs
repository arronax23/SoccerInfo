using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class ImagesUrlController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("League/{leagueId}.png")]
    public async Task<IActionResult> GetLeagueImage(int leagueId)
    {
        var base64Image = await queryDispatcher.Send(new GetLeagueImageQuery() { LeagueId = leagueId });

        var bytes = Convert.FromBase64String(base64Image);
        return File(bytes, "image/png");
    }
}
