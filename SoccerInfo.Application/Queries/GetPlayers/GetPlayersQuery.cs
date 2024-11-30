using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayers;

public class GetPlayersQuery : IQuery<IEnumerable<PlayerDto>>
{
    public int TeamId { get; set; }
}

