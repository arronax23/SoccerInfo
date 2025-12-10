using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Rapid.Application.Queries.GetPlayer;
using SoccerInfo.Rapid.Application.Queries.SearchPlayers;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class PlayersController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("players/{playerId}")]
    public async Task<PlayerDto?> GetPlayer(int playerId) => await queryDispatcher.Send(new GetPlayerQuery() { PlayerId = playerId });

    [HttpGet("players/search/{keyword}/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> SearchPlayers(
        [FromRoute] string keyword,
        [FromRoute] int pageNumber,
        [FromRoute] int pageSize)
    {
        if (pageNumber < 1)
            return BadRequest("Page number starts with 1");

        if (keyword == null || keyword.Length < 2)
            return BadRequest("Keyword must be at least two characters");

        return Ok(await queryDispatcher.Send(
            new SearchPlayersQuery()
            {
                Keyword = keyword,
                PageNumber = pageNumber,
                PageSize = pageSize
            }));
    }
}
