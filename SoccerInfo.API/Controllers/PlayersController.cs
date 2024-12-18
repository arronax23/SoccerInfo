using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.SearchPlayers;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class PlayersController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("api/SearchPlayers/{keyword}")]
    public async Task<IEnumerable<PlayerOverviewDto>> SearchPlayers(string keyword)
    {
        if (keyword == null || keyword.Length < 3)
            return null;

        return await queryDispatcher.Send(new SearchPlayersQuery() { Keyword = keyword });
    }
}
