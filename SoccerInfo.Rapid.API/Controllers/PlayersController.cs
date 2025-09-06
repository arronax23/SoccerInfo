using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Queries.GetLeague;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
[Route($"{RapidAPIConst.ApiPrefix}/{RapidAPIConst.Version}")]
public class PlayersController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("players/{playerId}")]
    public async Task<PlayerDto?> GetPlayer(int playerId) => await queryDispatcher.Send(new GetPlayerQuery() { PlayerId = playerId });

}
