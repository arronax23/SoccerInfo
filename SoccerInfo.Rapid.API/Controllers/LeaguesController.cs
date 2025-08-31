using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class LeaguesController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("GetLeague/{leagueId}")]
    public async Task<LeagueDto?> GetLeague(int leagueId) => await queryDispatcher.Send(new GetLeagueQuery() { LeagueId = leagueId });
}
