using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Commands.ExtarctBackend;
using SoccerInfo.Application.Commands.ExtarctPlayers;
using SoccerInfo.Application.Commands.UpdateTest;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class ExtractionController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPost]
    [Route("api/ExtarctPlayers")]
    public async Task<IActionResult> ExtarctPlayers()
    {
        await commandDispatcher.Send(new ExtarctPlayersCommand());
        return Ok();
    }

    [HttpPost]
    [Route("api/UpdateTest")]
    public async Task<IActionResult> UpdateTest()
    {
        await commandDispatcher.Send(new UpdateTestCommand());
        return Ok();
    }

    [HttpPost]
    [Route("api/ExtractBackend")]
    public async Task<IActionResult> ExtractBackend()
    {
        await commandDispatcher.Send(new ExtarctBackendCommand());
        return Ok();
    }
}
