using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctClubOverview;
public class ExtarctClubOverviewCommand : ICommand<IEnumerable<ClubOverview>>
{
    public IEnumerable<int> ClubsTransfermarktIds { get; set; } = null!;
}
