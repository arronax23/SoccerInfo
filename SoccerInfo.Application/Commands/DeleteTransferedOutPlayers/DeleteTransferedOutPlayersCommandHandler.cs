using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Application.Commands.DeleteTransferedOutPlayers;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctClubOverview;
internal class DeleteTransferedOutPlayersCommandHandler(
    ILogger<DeleteTransferedOutPlayersCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository,
    IGenericRepository<Transfer> transferRepository
    ) : ICommandHandler<DeleteTransferedOutPlayersCommand>
{
    public async Task Handle(DeleteTransferedOutPlayersCommand request, CancellationToken cancellationToken)
    {
        var transaction = unitOfWork.BeginTransaction();

        var playersWithLastTransfers = playerRepository.ToQuery().Select(p => new
        {
            Player = p,
            LastTransfer = p.Transfers.OrderByDescending(t => t.Date).First().To
        });

        var playersTransferredOut = playersWithLastTransfers.Where(p => p.LastTransfer!.ClubId != null).Select(p => p.Player);
        playerRepository.RemoveRange(playersTransferredOut);

        unitOfWork.ShowEntires();

        await unitOfWork.SaveChangesAsync();

        await unitOfWork.ResolveTransactionAsync(transaction);
    }
}
