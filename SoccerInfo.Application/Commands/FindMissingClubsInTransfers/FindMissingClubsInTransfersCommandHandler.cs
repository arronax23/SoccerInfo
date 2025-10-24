//using Microsoft.Extensions.Logging;
//using SoccerInfo.Application.Commands.ExtarctClubOverview;
//using SoccerInfo.Application.Abstractions.Interfaces;
//using SoccerInfo.Domain.Models;
//using SoccerInfo.Domain.Models.Transfers;
//using SoccerInfo.Domain.Repositories.Generic;
//using SoccerInfo.Shared.CQRS;
//using static SoccerInfo.Domain.Models.Transfers.Transfer;

//namespace SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
//internal class FindMissingClubsInTransfersCommandHandler(
//    ILogger<ExtarctClubOverviewCommandHandler> logger,
//    IUnitOfWork unitOfWork,
//    IGenericRepository<ClubInfo> clubInfoRepository,
//    IGenericRepository<Team> teamRepository,
//    IGenericRepository<ClubOverview> clubOverviewRepository,
//    ICommandDispatcher commandDispatcher) : ICommandHandler<FindMissingClubsInTransfersCommand>
//{
//    private readonly HashSet<int> searchClubsTransfermarktIds = new();
//    private readonly HashSet<ClubInfo> missing = new();
//    public async Task Handle(FindMissingClubsInTransfersCommand request, CancellationToken cancellationToken)
//    {
//        var clubInfos = clubInfoRepository
//            .ToQuery()
//            .Where(t => t.TeamId == null && t.ClubId == null);

//        foreach (var item in clubInfos)
//        {
//            var dbTeamId = teamRepository
//                .ToQuery()
//                .Where(t => t.TransfermarktId == item.ClubTransfermarktId)
//                .Select(t => t.Id)
//                .SingleOrDefault();

//            if (dbTeamId != 0)
//                item.AssignTeamById(dbTeamId);
//            else
//            {
//                var dbClubId = clubOverviewRepository
//                    .ToQuery()
//                    .Where(t => t.TransfermarktId == item.ClubTransfermarktId)
//                    .Select(t => t.Id)
//                    .SingleOrDefault();

//                if (dbClubId != 0)
//                    item.AssignClubById(dbClubId);
//                else
//                {
//                    searchClubsTransfermarktIds.Add(item.ClubTransfermarktId);
//                    missing.Add(item);
//                }
//            }
//        }


//        var clubsOverviews = 
//            (await commandDispatcher.Send(new ExtarctClubOverviewCommand() 
//            {
//                ClubsTransfermarktIds = searchClubsTransfermarktIds.Distinct()}
//            ))
//            .ToList();

//        foreach (var item in missing)
//        {
//            try
//            {
//                var club = clubsOverviews.SingleOrDefault(co => co.TransfermarktId == item.ClubTransfermarktId);
                
//                if (club is not null)
//                    item.AssignClub(club);
//            }
//            catch (Exception ex)
//            {
//                logger.LogCritical(ex.ToString());
//                logger.LogCritical($"item.From.ClubTransfermarktId = {item.ClubTransfermarktId}");
//            }

//        }

//        unitOfWork.ShowEntires();

//        await unitOfWork.SaveChangesAsync();

//        logger.LogInformation($"{nameof(FindMissingClubsInTransfersCommand)} has finished");
//    }
//}
