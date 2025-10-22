using Microsoft.AspNetCore.Mvc;
using SoccerInfo.API.Authorization;
using SoccerInfo.Application.Commands.DeleteTransferedOutPlayers;
using SoccerInfo.Application.Commands.SaveNationalities;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
[ApiKeyAuthorizationFilter]
public class HelperController(ICommandDispatcher commandDispatcher) : ControllerBase
{

    [HttpPut("api/SaveNationalities")]
    public async Task<IActionResult> SaveNationalities()
    {
        await commandDispatcher.Send(new SaveNationalitiesCommand());
        return Ok();
    }

    [HttpPut("api/DeleteTransferedOutPlayers")]
    public async Task<IActionResult> DeleteTransferedOutPlayers()
    {
        await commandDispatcher.Send(new DeleteTransferedOutPlayersCommand());
        return Ok();
    }


}
