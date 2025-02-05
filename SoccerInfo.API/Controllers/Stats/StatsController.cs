using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoccerInfo.API.Controllers.Stats.Requests;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.GetPlayersByStatsFilter;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.GetPlayersByStatsFilter.GetPlayersByStatsFilterQuery;

namespace SoccerInfo.API.Controllers.Stats;

[ApiController]
public class StatsController(IQueryDispatcher queryDispatcher, IMapper mapper) : ControllerBase
{
    [HttpPost("api/GetPlayersByStatsFilter")]
    public async Task<IEnumerable<PlayerOverviewDto>> GetPlayersByStatsFilter([FromBody] PlayerStatsFilterRequest filterRequest)
    {
        return await queryDispatcher.Send(new GetPlayersByStatsFilterQuery()
        {
            Filter = mapper.Map<PlayerStatsFilterDto>(filterRequest)
        });
    }
}
