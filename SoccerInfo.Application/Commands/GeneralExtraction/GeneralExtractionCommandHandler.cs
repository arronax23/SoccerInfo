using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.CalculatePlayerStatistics;
using SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
using SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;
using SoccerInfo.Application.Commands.ExtarctPlayersTransferHistory;
using SoccerInfo.Application.Commands.ExtractMarketValueProgress;
using SoccerInfo.Application.Commands.FindMissingClubsInTransfers;
using SoccerInfo.Application.Commands.SavePlayersCharacteristics;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.GeneralExtraction;

internal class GeneralExtractionCommandHandler(
    ILogger<GeneralExtractionCommandHandler> logger,
    ICommandDispatcher commandDispatcher, 
    IPlayerRepository repository) : ICommandHandler<GeneralExtractionCommand>
{
    public async Task Handle(GeneralExtractionCommand request, CancellationToken cancellationToken)
    {
        await ExtractAndSavePlayersGeneralInfo();
        await ExtractAndSavePlayersCharacteristics();
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

    private async Task ExtractAndSavePlayersCharacteristics()
    {
        var count = await repository.GetPlayersWithoutCharacteristicsCount();
        var batchSize = 100;
        var totalBatches = Math.Ceiling((double)count / batchSize);

        for (int i = 0; i < totalBatches; i++)
        {
            var playersCharacteristicsData = await commandDispatcher.Send(new ExtractPlayersCharacteristicsCommand()
            {
                OnlyNewPlayers = true,
                PlayerCount = batchSize,
                Skip = batchSize * i
            });
            await commandDispatcher.Send(new SavePlayersCharacteristicsCommand() { Extraction = playersCharacteristicsData! });
            logger.LogInformation($"{nameof(ExtractAndSavePlayersCharacteristics)} success - Players updated: {batchSize * (i + 1)} /{count}");
            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}
