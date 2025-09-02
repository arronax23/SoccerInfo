using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Rapid.Application.Queries.GetLeagueTeams;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class LeaguesController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("leagues/{leagueId}")]
    public async Task<LeagueDto?> GetLeague(int leagueId) => await queryDispatcher.Send(new GetLeagueQuery() { LeagueId = leagueId });
    [HttpGet("leagues/{leagueId}/teams")]
    public async Task<LeagueTeamsDto?> GetLeagueTeams(int leagueId) => await queryDispatcher.Send(new GetLeagueTeamsQuery() { LeagueId = leagueId });
}
