using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoccerInfo.API.Authorization;
using SoccerInfo.Application.Commands.ExtarctBackend;
using SoccerInfo.Application.Commands.ExtarctPlayers;
using SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
using SoccerInfo.Application.Commands.SavePlayersCharacteristics;
using SoccerInfo.Application.Commands.SaveTransfermarktCookie;
using SoccerInfo.Application.Commands.UpdateTest;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
[ApiKeyAuthorizationFilter]
public class ExtractionController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPut("api/ExtarctPlayers")]
    public async Task<IActionResult> ExtarctPlayers()
    {
        await commandDispatcher.Send(new ExtarctPlayersCommand());
        return Ok();
    }

    [HttpPut("api/ExtarctPlayersCharacteristics")]
    public async Task<IActionResult> ExtarctPlayersCharacteristics(
        int playerCount, 
        bool onlyNewPlayers, 
        CancellationToken cancellationToken)
    {
        await commandDispatcher.Send(new ExtarctPlayersCharacteristicsCommand()
        {
            PlayerCount  = playerCount,
            OnlyNewPlayers = onlyNewPlayers
        }, 
        cancellationToken);
        return Ok();
    }

    [HttpPut("api/SavePlayersCharacteristics")]
    public async Task<IActionResult> SavePlayersCharacteristics(string fileName)
    {
        if (fileName.IsNullOrEmpty())
            return BadRequest();

        await commandDispatcher.Send(new SavePlayersCharacteristicsCommand()
        {
            FileName = fileName
        });
        return Ok();
    }

    [HttpPut("api/SaveTransfermarktCookie")]
    public async Task<IActionResult> SaveTransfermarktCookie()
    {
        await commandDispatcher.Send(new SaveTransfermarktCookieCommand());
        return Ok();
    }

    [HttpPut("api/UpdateTest")]
    public async Task<IActionResult> UpdateTest()
    {
        await commandDispatcher.Send(new UpdateTestCommand());
        return Ok();
    }

    [HttpPut("api/ExtractBackend")]
    public async Task<IActionResult> ExtractBackend()
    {
        await commandDispatcher.Send(new ExtarctBackendCommand());
        return Ok();
    }

}
