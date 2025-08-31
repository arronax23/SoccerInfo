using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.GetLeague;
using SoccerInfo.Application.Queries.GetLeagues;
using SoccerInfo.Application.Queries.GetPlayerCharacteristics;
using SoccerInfo.Application.Queries.GetPlayerDetails;
using SoccerInfo.Application.Queries.GetGroupedPlayers;
using SoccerInfo.Application.Queries.GetPlayerTransferHistory;
using SoccerInfo.Application.Queries.GetTeam;
using SoccerInfo.Application.Queries.GetTeams;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfoWeb.API.Controllers;

[ApiController]
public class LeaguesController(IQueryDispatcher queryDispatcher) : ControllerBase
{
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

    [HttpGet("api/GetGroupedPlayers/{teamId}")]
    public async Task<IEnumerable<PlayersGroupDto>> GetPlayers(int teamId)
    {
        return await queryDispatcher.Send(new GetGroupedPlayersQuery() { TeamId = teamId });
    }

    [HttpGet("api/GetPlayerDetails/{playerId}")]
    public async Task<PlayerDetailsDto> GetPlayerDetails(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerDetailsQuery() { PlayerId = playerId });
    }

    [HttpGet("api/GetPlayerCharacteristics/{playerId}")]
    public async Task<PlayerCharacteristicsDto?> GetPlayerCharacteristics(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerCharacteristicsQuery() { PlayerId = playerId });
    }

    [HttpGet("api/GetPlayerTransferHistory/{playerId}")]
    public async Task<PlayerTransferHistoryDto?> GetPlayerTransferHistory(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerTransferHistoryQuery() { PlayerId = playerId });
    }
}
