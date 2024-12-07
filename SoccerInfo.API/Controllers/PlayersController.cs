using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Commands.ExtarctPlayers;
using SoccerInfo.Application.Commands.UpdateTest;
using SoccerInfo.Application.Queries.GetPlayers;
using SoccerInfo.Application.Queries.GetTeams;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class PlayersController(
    IQueryDispatcher queryDispatcher,
    ICommandDispatcher commandDispatcher
    ) : ControllerBase
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

    [HttpGet("api/GetTeams")]
    public async Task<IEnumerable<TeamDto>> GetTeams()
    {
        return await queryDispatcher.Send(new GetTeamsQuery());
    }

    [HttpGet("api/GetPlayers/{teamId}")]
    public async Task<IEnumerable<PlayerDto>> GetPlayers(int teamId)
    {
        return await queryDispatcher.Send(new GetPlayersQuery() { TeamId = teamId });
    }
}
