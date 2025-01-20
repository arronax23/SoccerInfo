using Microsoft.AspNetCore.Mvc;
using SoccerInfo.API.Authorization;
using SoccerInfo.Application.Commands.ChangeFlagsToSvg;
using SoccerInfo.Application.Commands.UpdateToEnglish;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
[ApiKeyAuthorizationFilter]
public class HelperController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPut("api/ChangeFlagsToSvg")]
    public async Task<IActionResult> ChangeFlagsToSvg()
    {
        await commandDispatcher.Send(new ChangeFlagsToSvgCommand());
        return Ok();
    }

    [HttpPut("api/UpdateToEnglish")]
    public async Task<IActionResult> UpdateToEnglish()
    {
        await commandDispatcher.Send(new UpdateToEnglishCommand());
        return Ok();
    }

}
