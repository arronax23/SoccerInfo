using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetGroupedPlayers;

public class GetGroupedPlayersQuery : IQuery<IEnumerable<PlayersGroupDto>>
{
    public int TeamId { get; set; }
}

