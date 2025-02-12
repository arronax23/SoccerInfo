using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoccerInfo.API.Authorization;
using SoccerInfo.API.Controllers.Stats.Requests;
using SoccerInfo.Application.Commands.CalculatePlayerStatistics;
using SoccerInfo.Application.Queries.GetPlayersByStats;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.GetPlayersByStats.GetPlayersByStatsQuery;

namespace SoccerInfo.API.Controllers.Stats;

[ApiController]
public class StatsController(
    IQueryDispatcher queryDispatcher,
    ICommandDispatcher commandDispatcher,
    IMapper mapper) : ControllerBase
{
    [HttpPost("api/GetPlayersByStats")]
    public async Task<IActionResult> GetPlayersByStatsFilter([FromBody] PlayerStatsFilterRequest filterRequest)
    {
        if (filterRequest.PageNumber < 1)
            return BadRequest("Page number starts with 1");

        var statsPlayers = await queryDispatcher.Send(new GetPlayersByStatsQuery()
        {
            Filter = mapper.Map<PlayerStatsFilterDto>(filterRequest)
        });

        return Ok(statsPlayers);
    }

    [ApiKeyAuthorizationFilter]
    [HttpPost("api/CalculatePlayerStatistics")]
    public async Task<IActionResult> CalculatePlayerStatistics()
    {
        await commandDispatcher.Send(new CalculatePlayerStatisticsCommand());

        return Ok();
    }
}
