using SoccerInfo.Application.Commands.CalculatePlayerStatistics;
using SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
using SoccerInfo.Application.Commands.ExtarctPlayersGeneralnfo;
using SoccerInfo.Application.Commands.ExtractMarketValueProgress;
using SoccerInfo.Application.Commands.SavePlayersCharacteristics;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.GeneralExtraction;

internal class GeneralExtractionCommandHandler(ICommandDispatcher commandDispatcher)
    : ICommandHandler<GeneralExtractionCommand>
{
    public async Task Handle(GeneralExtractionCommand request, CancellationToken cancellationToken)
    {
        var playersGeneralInfoData = await commandDispatcher.Send(new ExtarctPlayersGeneralnfoCommand());
        await commandDispatcher.Send(new SavePlayersGeneralInfoCommand() { Extraction = playersGeneralInfoData! });
        var playersCharacteristicsData = await commandDispatcher.Send(new ExtarctPlayersCharacteristicsCommand()
        {
            OnlyNewPlayers = false,
            PlayerCount = int.MaxValue
        });
        await commandDispatcher.Send(new SavePlayersCharacteristicsCommand() { Extraction = playersCharacteristicsData! });
        await commandDispatcher.Send(new ExtractMarketValueProgressCommand());
        await commandDispatcher.Send(new CalculatePlayerStatisticsCommand());
    }
}
