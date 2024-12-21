using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Commands.ChangeFlagsToSvg;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class HelperController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPost]
    [Route("api/ChangeFlagsToSvg")]
    public async Task<IActionResult> ChangeFlagsToSvg()
    {
        await commandDispatcher.Send(new ChangeFlagsToSvgCommand());
        return Ok();
    }

}
