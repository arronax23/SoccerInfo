using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.ExtarctClubOverview;
using SoccerInfo.Application.Intrefaces;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
internal class FindMissingClubsInTransfersCommandHandler(
    ILogger<ExtarctClubOverviewCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IGenericRepository<Transfer> transferRepository,
    IGenericRepository<Team> teamRepository,
    IGenericRepository<ClubOverview> clubOverviewRepository,
    ICommandDispatcher commandDispatcher) : ICommandHandler<FindMissingClubsInTransfersCommand>
{
    private readonly List<int> searchClubsTransfermarktIds = new();
    private readonly List<Transfer> missingFrom = new();
    private readonly List<Transfer> missingTo = new();
    public async Task Handle(FindMissingClubsInTransfersCommand request, CancellationToken cancellationToken)
    {
        var from = transferRepository
            .ToQuery()
            .Where(t => t.From!.TeamId == null && t.From.ClubId == null);

        var to = transferRepository
            .ToQuery()
            .Where(t => t.To!.TeamId == null && t.To.ClubId == null);

        foreach (var item in from)
        {
            var dbTeam = teamRepository.ToQuery().SingleOrDefault(t => t.TransfermarktId == item.From.ClubTransfermarktId);

            if (dbTeam is not null)
                item.From!.AssignTeam(dbTeam);
            else
            {
                var dbClub = clubOverviewRepository.ToQuery().SingleOrDefault(co => co.TransfermarktId == item.From.ClubTransfermarktId);
               
                if (dbClub is not null)
                    item.From!.AssignClub(dbClub);
                else
                {
                    searchClubsTransfermarktIds.Add(item.From.ClubTransfermarktId);
                    missingFrom.Add(item);
                }
            }
        }

        foreach (var item in to)
        {
            var dbTeam = teamRepository.ToQuery().SingleOrDefault(t => t.TransfermarktId == item.To.ClubTransfermarktId);

            if (dbTeam is not null)
                item.To!.AssignTeam(dbTeam);
            else
            {
                var dbClub = clubOverviewRepository.ToQuery().SingleOrDefault(co => co.TransfermarktId == item.To.ClubTransfermarktId);

                if (dbClub is not null)
                    item.To!.AssignClub(dbClub);
                else
                {
                    searchClubsTransfermarktIds.Add(item.To.ClubTransfermarktId);
                    missingTo.Add(item);
                }
            }
        }

        var clubsOverviews = 
            (await commandDispatcher.Send(new ExtarctClubOverviewCommand() 
            {
                ClubsTransfermarktIds = searchClubsTransfermarktIds.Distinct()}
            ))
            .ToList();

        foreach (var item in missingFrom)
            item.From!.AssignClub(clubsOverviews.Single(co => co.TransfermarktId == item.From.ClubTransfermarktId));

        foreach (var item in missingTo)
            item.To!.AssignClub(clubsOverviews.Single(co => co.TransfermarktId == item.To.ClubTransfermarktId));


        unitOfWork.ShowEntires();

        await unitOfWork.SaveChangesAsync();



        logger.LogInformation($"{nameof(FindMissingClubsInTransfersCommand)} has finished");
    }
}
