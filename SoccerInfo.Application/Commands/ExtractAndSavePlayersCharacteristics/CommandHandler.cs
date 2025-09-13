using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SoccerInfo.Application.Commands.ExtractPlayersCharacteristics;
using SoccerInfo.Application.Commands.SavePlayersCharacteristics;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtractAndSavePlayersCharacteristics;
internal class CommandHandler(
    ILogger<CommandHandler> logger,
    IConfiguration configuration,
    ICommandDispatcher commandDispatcher,
    IPlayerRepository playerRepository) : ICommandHandler<ExtractAndSavePlayersCharacteristicsCommand>
{
    private readonly int _batchSize = configuration.GetValue<int>("ExtractAndSavePlayersCharacteristicsCommand:BatchSize");   
    private int _progressCounter = 0;   

    public async Task Handle(ExtractAndSavePlayersCharacteristicsCommand request, CancellationToken cancellationToken)
    {
        var playersIds = playerRepository.ToQuery().OrderBy(p => p.Id).Select(p => p.Id).ToList();

        foreach (var batchIds in playersIds.Chunk(_batchSize))
        {
            var extractionData = await commandDispatcher.Send(new ExtractPlayersCharacteristicsCommand() { PlayersIds = batchIds });

            if (extractionData is null)
                throw new Exception("Missing extracted data");
            
            await commandDispatcher.Send(new SavePlayersCharacteristicsCommand() { Extraction = extractionData });

            _progressCounter += _batchSize;
            logger.LogInformation($"{nameof(ExtractAndSavePlayersCharacteristicsCommand)} extracted and saved {_progressCounter}/{playersIds.Count()}");
        }
    }
}
