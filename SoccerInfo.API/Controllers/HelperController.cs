using Microsoft.AspNetCore.Mvc;
using SoccerInfo.API.Authorization;
using SoccerInfo.Application.Commands.ChangeFlagsToSvg;
using SoccerInfo.Application.Commands.SaveNationalities;
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

    [HttpPut("api/SaveNationalities")]
    public async Task<IActionResult> SaveNationalities()
    {
        await commandDispatcher.Send(new SaveNationalitiesCommand());
        return Ok();
    }

}
