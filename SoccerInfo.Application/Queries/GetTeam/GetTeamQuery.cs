using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetTeam;

public class GetTeamQuery : IQuery<TeamDto>
{
    public int TeamId { get; set; }
}

