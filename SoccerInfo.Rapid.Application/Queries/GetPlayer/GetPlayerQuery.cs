using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;

public class GetPlayerQuery : IQuery<PlayerDto?>
{
    public int PlayerId { get; set; }
}
