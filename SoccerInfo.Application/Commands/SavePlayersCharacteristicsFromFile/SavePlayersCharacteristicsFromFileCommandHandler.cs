using SoccerInfo.Application.Commands.SavePlayersCharacteristics;
using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristicsFromFile;

internal class SavePlayersCharacteristicsFromFileCommandHandler(
    ICommandDispatcher commandDispatcher) : ICommandHandler<SavePlayersCharacteristicsFromFileCommand>
{
    public async Task Handle(SavePlayersCharacteristicsFromFileCommand request, CancellationToken cancellationToken)
    {
        string fileName = request.FileName.Trim();
        string jsonString = File.ReadAllText(Path.Combine("JsonData", fileName));

        PlayersCharacteristicsExtractionData extraction =
            JsonSerializer.Deserialize<PlayersCharacteristicsExtractionData>(jsonString)!;

        await commandDispatcher.Send(new SavePlayersCharacteristicsCommand()
        {
            Extraction = extraction,
        });
    }
}
