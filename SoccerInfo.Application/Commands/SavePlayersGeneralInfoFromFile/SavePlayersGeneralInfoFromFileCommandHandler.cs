using Microsoft.Extensions.Configuration;
using SoccerInfo.Application.Commands.SavePlayersGeneralInfoFromFile;
using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Shared.CQRS;
using System.Text.Json;

namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;

internal class SavePlayersGeneralInfoFromFileCommandHandler(
    ICommandDispatcher commandDispatcher) : ICommandHandler<SavePlayersGeneralInfoFromFileCommand>
{
    public async Task Handle(SavePlayersGeneralInfoFromFileCommand request, CancellationToken cancellationToken)
    {
        string fileName = request.FileName.Trim();
        string jsonString = File.ReadAllText(Path.Combine("JsonData", fileName));

        GeneralInfoExtractionData extraction = JsonSerializer.Deserialize<GeneralInfoExtractionData>(jsonString)!;

        await commandDispatcher.Send(new SavePlayersGeneralInfoCommand()
        {
            Extraction = extraction
        });
    }
}
