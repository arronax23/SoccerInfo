using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models.Transfers;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayerTransferHistoryDto;
using static SoccerInfo.Application.Queries.Dtos.PlayerTransferHistoryDto.TransferDto;

namespace SoccerInfo.Application.Queries.GetPlayerTransferHistory;
internal class GetPlayerTransferHistoryQueryHandler(IPlayerRepository playerRepository)
    : IQueryHandler<GetPlayerTransferHistoryQuery, PlayerTransferHistoryDto?>
{
    public async Task<PlayerTransferHistoryDto?> Handle(GetPlayerTransferHistoryQuery request, CancellationToken cancellationToken)
    {
        var player = await playerRepository.SingleOrDefaultAsync(p => p.Id == request.PlayerId);

        if (player is null)
            return null;

        var transfers = player.Transfers;

        if (transfers is null || transfers.Count == 0)
            return null;

        var transfersDto = transfers
            .OrderBy(t => t.Date ?? DateTime.MinValue)
            .ThenBy(t => t.Age ?? 0)
            .Select(t => new TransferDto()
            {
                Age = t.Age,
                Date = t.Date,
                Fee = MoneyDto.Create(t.Fee?.Value, t.Fee?.Suffix),
                Season = t.Season,
                MarketValue = MoneyDto.Create(t.MarketValue?.Value, t.MarketValue?.Suffix),
                ClubFromName = t.From!.Team?.Name ?? t.From.Club!.Name,
                ClubToName = t.To!.Team?.Name ?? t.To.Club!.Name,
                CLubFromBase64Image = t.From!.Team?.Logo.Base64 ?? t.From.Club!.ClubImageBase64,
                CLubToBase64Image = t.To!.Team?.Logo.Base64 ?? t.To.Club!.ClubImageBase64,
                TransferType = MapTransferType(t.Type)
            });

        return new PlayerTransferHistoryDto(){ Transfers = transfersDto };
    }


    private string MapTransferType(TransferType type)
    {
        return type switch
        {
            TransferType.Sold => "Sold",
            TransferType.Loaned => "Loan",
            TransferType.ReturnedFromLoan => "Loan End",
            _ => throw new NotImplementedException()    
        };
    }

}
