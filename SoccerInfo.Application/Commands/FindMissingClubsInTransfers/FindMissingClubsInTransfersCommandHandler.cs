using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.ExtarctClubOverview;
using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Domain.Models.Transfers.Transfer;

namespace SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
internal class FindMissingClubsInTransfersCommandHandler(
    ILogger<ExtarctClubOverviewCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IGenericRepository<Transfer> transferRepository,
    IGenericRepository<Team> teamRepository,
    IGenericRepository<ClubOverview> clubOverviewRepository,
    ICommandDispatcher commandDispatcher) : ICommandHandler<FindMissingClubsInTransfersCommand>
{
    public async Task Handle(FindMissingClubsInTransfersCommand request, CancellationToken cancellationToken)
    {
        var transaction = unitOfWork.BeginTransaction();

        var transfers = transferRepository
            .ToQuery()
            .Where(t =>
                (t.From!.TeamId == null && t.From.ClubId == null) ||
                (t.To!.TeamId == null && t.To.ClubId == null))
            .ToList();

        var missingClubTransfermarktIds = new HashSet<int>();

        foreach (var transfer in transfers)
        {
            ProcessClubInfo(transfer.From!, teamRepository, clubOverviewRepository, missingClubTransfermarktIds);
            ProcessClubInfo(transfer.To!, teamRepository, clubOverviewRepository, missingClubTransfermarktIds);
        }

        var clubsOverviews =
            (await commandDispatcher.Send(new ExtarctClubOverviewCommand()
            {
                ClubsTransfermarktIds = missingClubTransfermarktIds
            }))
            .ToList();

        foreach (var transfer in transfers)
        {
            TryAssignFromClubs(transfer.From!, clubsOverviews, logger);
            TryAssignFromClubs(transfer.To!, clubsOverviews, logger);
        }

        unitOfWork.ShowEntires();
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.ResolveTransactionAsync(transaction);
        logger.LogInformation($"{nameof(FindMissingClubsInTransfersCommand)} has finished");
    }

    private static void ProcessClubInfo(
        ClubInfo clubInfo,
        IGenericRepository<Team> teamRepository,
        IGenericRepository<ClubOverview> clubOverviewRepository,
        HashSet<int> missingIds)
    {
        var dbTeamId = teamRepository
            .ToQuery()
            .Where(t => t.TransfermarktId == clubInfo.ClubTransfermarktId)
            .Select(t => t.Id)
            .SingleOrDefault();

        if (dbTeamId != 0)
            clubInfo.AssignTeamById(dbTeamId);
        else
        {
            var dbClubId = clubOverviewRepository
                .ToQuery()
                .Where(c => c.TransfermarktId == clubInfo.ClubTransfermarktId)
                .Select(c => c.Id)
                .SingleOrDefault();

            if (dbClubId != 0)
                clubInfo.AssignClubById(dbClubId);
            else
                missingIds.Add(clubInfo.ClubTransfermarktId);
        }
    }

    private static void TryAssignFromClubs(ClubInfo clubInfo, List<ClubOverview> clubsOverviews, ILogger logger)
    {
        try
        {
            var club = clubsOverviews.SingleOrDefault(c => c.TransfermarktId == clubInfo.ClubTransfermarktId);
            if (club is not null)
                clubInfo.AssignClub(club);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, $"Error assigning club for {clubInfo.ClubTransfermarktId}");
        }
    }
}
