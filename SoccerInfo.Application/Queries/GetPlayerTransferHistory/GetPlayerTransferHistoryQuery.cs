using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayerTransferHistory;
public class GetPlayerTransferHistoryQuery : IQuery<PlayerTransferHistoryDto?>
{
    public int PlayerId { get; set; }
}
