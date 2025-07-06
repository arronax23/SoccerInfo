using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Intrefaces;
using SoccerInfo.BackendScraper.PlayersTransfers;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.BackendScraper.PlayersTransfers.PlayersTransfersScraper.PlayerTransfersData;
using static SoccerInfo.Domain.Models.Transfers.Transfer;

namespace SoccerInfo.Application.Commands.ExtarctPlayersTransferHistory;

public class ExtarctPlayersTransferHistoryCommandHandler(
    ILogger<ExtarctPlayersTransferHistoryCommandHandler> logger,
    PlayersTransfersScraper scraper,
    IUnitOfWork unitOfWork,
    IPlayerRepository playerRepository,
    IGenericRepository<Team> teamRepository) : ICommandHandler<ExtarctPlayersTransferHistoryCommand>
{
    public async Task Handle(ExtarctPlayersTransferHistoryCommand request, CancellationToken cancellationToken)
    {
        var players = playerRepository.ToQuery().Take(100);

        var data = await scraper.Scrape(players.Select(p => p.TransfermarktId));

        foreach (var extractedTransfers in data)
        {
            var player = players.Single(p => p.TransfermarktId == extractedTransfers.PlayerTransfermarktId);
            var transfers = extractedTransfers.Transfers
                .Select(t => MapTransfer(t, extractedTransfers.PlayerTransfermarktId))
                .OrderBy(tr => tr.Date);

            player.AddNewTransfers(transfers);
        }

        unitOfWork.SaveChanges();

        logger.LogInformation($"{nameof(ExtarctPlayersTransferHistoryCommand)} has finished");
    }


    private Transfer MapTransfer(TransferItemData transferItem, int playerTransfermarktId)
    {
        var clubInfoFrom = ClubInfo.Create(transferItem.ClubFrom.TransfermarktId);
        var clubInfoTo = ClubInfo.Create(transferItem.ClubTo.TransfermarktId);

        var teamFrom = teamRepository.ToQuery().SingleOrDefault(t => t.TransfermarktId == transferItem.ClubFrom.TransfermarktId);
        var teamTo = teamRepository.ToQuery().SingleOrDefault(t => t.TransfermarktId == transferItem.ClubTo.TransfermarktId);
        
        if (teamFrom is not null)
            clubInfoFrom.AssignTeam(teamFrom);

        if (teamTo is not null)
            clubInfoTo.AssignTeam(teamTo);

        return new Transfer()
        {
            Age = transferItem.Details.Age,
            Date = DateTime.Parse(transferItem.Details.Date),
            FeeNormalized = transferItem.Details.FeeNormalized,
            MarketValueNormalized = transferItem.Details.MarketValueNormalized,
            Fee = new Money(transferItem.Details?.Fee?.Value, transferItem.Details?.Fee?.Suffix),
            MarketValue = new Money(transferItem.Details?.MarketValue?.Value, transferItem.Details?.MarketValue?.Suffix),
            PlayerTransferMarktId = playerTransfermarktId,
            From = clubInfoFrom,
            To = clubInfoTo,
            Season = transferItem.Details.Season,
            Type = MapTransferType(transferItem.Details.Type)
        };
    }
    private TransferType MapTransferType(string transferType)
    {
        return transferType switch
        {
            "STANDARD" => TransferType.Sold,
            "ACTIVE_LOAN_TRANSFER" => TransferType.Loaned,
            "RETURNED_FROM_PREVIOUS_LOAN" => TransferType.ReturnedFromLoan,
            _ => throw new Exception($"TransferType of {transferType} is not valid")
        };
    }
}
