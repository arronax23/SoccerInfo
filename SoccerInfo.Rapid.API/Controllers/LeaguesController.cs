using Microsoft.AspNetCore.Mvc;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Application.Queries.GetLeague;
using SoccerInfo.Application.Queries.GetLeagues;
using SoccerInfo.Application.Queries.GetPlayerCharacteristics;
using SoccerInfo.Application.Queries.GetPlayerDetails;
using SoccerInfo.Application.Queries.GetPlayers;
using SoccerInfo.Application.Queries.GetPlayerTransferHistory;
using SoccerInfo.Application.Queries.GetTeam;
using SoccerInfo.Application.Queries.GetTeams;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;

[ApiController]
public class LeaguesController(IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("rapidapi/GetTeams/{leagueId}")]
    public async Task<IEnumerable<TeamDto>> GetTeams(int leagueId)
    {
        return await queryDispatcher.Send(new GetTeamsQuery() { LeagueId = leagueId });
    }

    [HttpGet("rapidapi/GetTeam/{teamId}")]
    public async Task<TeamDto> GetTeam(int teamId)
    {
        return await queryDispatcher.Send(new GetTeamQuery() { TeamId = teamId });
    }

    [HttpGet("rapidapi/GetLeague/{leagueId}")]
    public async Task<LeagueDto> GetLeague(int leagueId)
    {
        return await queryDispatcher.Send(new GetLeagueQuery() { LeagueId = leagueId });
    }

    [HttpGet("rapidapi/GetLeagues")]
    public async Task<IEnumerable<LeagueDto>> GetLeagues()
    {
        return await queryDispatcher.Send(new GetLeaguesQuery());
    }

    [HttpGet("rapidapi/GetGroupedPlayers/{teamId}")]
    public async Task<IEnumerable<PlayersGroupDto>> GetPlayers(int teamId)
    {
        return await queryDispatcher.Send(new GetGroupedPlayersQuery() { TeamId = teamId });
    }

    [HttpGet("rapidapi/GetPlayerDetails/{playerId}")]
    public async Task<PlayerDetailsDto> GetPlayerDetails(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerDetailsQuery() { PlayerId = playerId });
    }

    [HttpGet("rapidapi/GetPlayerCharacteristics/{playerId}")]
    public async Task<PlayerCharacteristicsDto?> GetPlayerCharacteristics(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerCharacteristicsQuery() { PlayerId = playerId });
    }

    [HttpGet("rapidapi/GetPlayerTransferHistory/{playerId}")]
    public async Task<PlayerTransferHistoryDto?> GetPlayerTransferHistory(int playerId)
    {
        return await queryDispatcher.Send(new GetPlayerTransferHistoryQuery() { PlayerId = playerId });
    }
}
