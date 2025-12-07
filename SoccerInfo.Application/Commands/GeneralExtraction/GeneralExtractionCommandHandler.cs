using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.CalculatePlayerStatistics;
using SoccerInfo.Application.Commands.ExtractPlayersCharacteristics;
using SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;
using SoccerInfo.Application.Commands.ExtarctPlayersTransferHistory;
using SoccerInfo.Application.Commands.ExtractMarketValueProgress;
using SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Application.Commands.ExtractAndSavePlayersCharacteristics;

namespace SoccerInfo.Application.Commands.GeneralExtraction;

internal class GeneralExtractionCommandHandler(
    ILogger<GeneralExtractionCommandHandler> logger,
    ICommandDispatcher commandDispatcher, 
    IPlayerRepository repository) : ICommandHandler<GeneralExtractionCommand>
{
    public async Task Handle(GeneralExtractionCommand request, CancellationToken cancellationToken)
    {
        await ExtractAndSavePlayersGeneralInfo();
        await commandDispatcher.Send(new ExtractAndSavePlayersCharacteristicsCommand());
        await commandDispatcher.Send(new ExtractMarketValueProgressCommand());
        await commandDispatcher.Send(new ExtarctPlayersTransferHistoryCommand());
        await commandDispatcher.Send(new FindMissingClubsInTransfersCommand());
        await commandDispatcher.Send(new CalculatePlayerStatisticsCommand());
    }

    private async Task ExtractAndSavePlayersGeneralInfo()
    {
        var playersGeneralInfoData = await commandDispatcher.Send(new ExtarctPlayersGeneralnfoCommand());
        await commandDispatcher.Send(new SavePlayersGeneralInfoCommand() { Extraction = playersGeneralInfoData! });
    }
}
