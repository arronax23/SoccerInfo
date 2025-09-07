using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Queries.SearchPlayers;
using SoccerInfo.Application.Queries.SearchTeams;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class SearchController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("api/SearchPlayers/{keyword}/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> SearchPlayers(string keyword, int pageNumber, int pageSize)
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

    [HttpGet("api/SearchTeams/{keyword}/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> SearchTeams(string keyword, int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
            return BadRequest("Page number starts with 1");

        if (keyword == null || keyword.Length < 2)
            return NoContent();

        return Ok(await queryDispatcher.Send(
            new SearchTeamsQuery()
            {
                Keyword = keyword,
                PageNumber = pageNumber,
                PageSize = pageSize
            }));
    }
}
