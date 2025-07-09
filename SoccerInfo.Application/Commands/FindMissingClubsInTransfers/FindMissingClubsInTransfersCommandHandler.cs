using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.ExtarctClubOverview;
using SoccerInfo.Application.Intrefaces;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
internal class FindMissingClubsInTransfersCommandHandler(
    ILogger<ExtarctClubOverviewCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IGenericRepository<Transfer> transferRepository,
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
            .Where(t => t.From.TeamId == null && t.From.ClubId == null);

        var to = transferRepository
            .ToQuery()
            .Where(t => t.To.TeamId == null && t.To.ClubId == null);

        foreach (var item in from)
        {
            var dbClub = clubOverviewRepository.ToQuery().SingleOrDefault(co => co.TransfermarktId == item.From.ClubTransfermarktId);

            if (dbClub is not null)
                item.From.AssignClub(dbClub);
            else
            {
                searchClubsTransfermarktIds.Add(item.From.ClubTransfermarktId);
                missingFrom.Add(item);
            }
        }

        foreach (var item in to)
        {
            var dbClub = clubOverviewRepository.ToQuery().SingleOrDefault(co => co.TransfermarktId == item.To.ClubTransfermarktId);

            if (dbClub is not null)
                item.To.AssignClub(dbClub);
            else
            {
                searchClubsTransfermarktIds.Add(item.To.ClubTransfermarktId);
                missingTo.Add(item);
            }
        }

        var clubsOverviews = await commandDispatcher.Send(new ExtarctClubOverviewCommand() { ClubsTransfermarktIds = searchClubsTransfermarktIds.Distinct()});

        clubsOverviews = clubsOverviews.ToList();   

        foreach (var co in clubsOverviews)
        {
            unitOfWork.Entry(co).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        Console.WriteLine($"Unique clubs to add: {clubsOverviews.Count()}");

        foreach (var club in clubsOverviews.GroupBy(c => c.TransfermarktId).Where(g => g.Count() > 1))
        {
            Console.WriteLine($"DUPLICATE in memory: {club.Key} - {club.Count()} times");
        }

        //clubOverviewRepository.AttachRange(clubsOverviews);

        foreach (var item in missingFrom)
        {
            try
            {
                Console.WriteLine(item.From.ClubTransfermarktId);
                item.From.AssignClub(clubsOverviews.Single(co => co.TransfermarktId == item.From.ClubTransfermarktId));
            }
            catch (Exception)
            {

                throw;
            }
 
        }

        foreach (var item in missingTo)
        {

            try
            {
                Console.WriteLine(item.To.ClubTransfermarktId);
                item.To.AssignClub(clubsOverviews.Single(co => co.TransfermarktId == item.To.ClubTransfermarktId));
            }
            catch (Exception)
            {

                throw;
            }

        }

        unitOfWork.ShowEntires();

        await unitOfWork.SaveChangesAsync();



        logger.LogInformation($"{nameof(FindMissingClubsInTransfersCommand)} has finished");
    }
}
