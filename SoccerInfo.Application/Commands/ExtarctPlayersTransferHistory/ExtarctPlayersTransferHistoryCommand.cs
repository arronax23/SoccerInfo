using SoccerInfo.Shared.CQRS;
namespace SoccerInfo.Application.Commands.ExtarctPlayersTransferHistory;

public class ExtarctPlayersTransferHistoryCommand : ICommand
{
    public int PlayersSize { get; set; }
}
