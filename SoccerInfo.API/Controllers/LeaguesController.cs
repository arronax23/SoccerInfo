using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Commands.ExtarctPlayers;
using SoccerInfo.Application.Commands.UpdateTest;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.GetLeague;
using SoccerInfo.Application.Queries.GetLeagues;
using SoccerInfo.Application.Queries.GetPlayers;
using SoccerInfo.Application.Queries.GetTeam;
using SoccerInfo.Application.Queries.GetTeams;
using SoccerInfo.Shared.CQRS;
namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class LeaguesController(
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

    [HttpGet("api/GetTeams/{leagueId}")]
    public async Task<IEnumerable<TeamDto>> GetTeams(int leagueId)
    {
        return await queryDispatcher.Send(new GetTeamsQuery() { LeagueId = leagueId });
    }

    [HttpGet("api/GetTeam/{teamId}")]
    public async Task<TeamDto> GetTeam(int teamId)
    {
        return await queryDispatcher.Send(new GetTeamQuery() { TeamId = teamId });
    }

    [HttpGet("api/GetLeague/{leagueId}")]
    public async Task<LeagueDto> GetLeague(int leagueId)
    {
        return await queryDispatcher.Send(new GetLeagueQuery() { LeagueId = leagueId });
    }

    [HttpGet("api/GetLeagues")]
    public async Task<IEnumerable<LeagueDto>> GetLeagues()
    {
        return await queryDispatcher.Send(new GetLeaguesQuery());
    }

    [HttpGet("api/GetPlayers/{teamId}")]
    public async Task<IEnumerable<PlayerDto>> GetPlayers(int teamId)
    {
        return await queryDispatcher.Send(new GetPlayersQuery() { TeamId = teamId });
    }
}
