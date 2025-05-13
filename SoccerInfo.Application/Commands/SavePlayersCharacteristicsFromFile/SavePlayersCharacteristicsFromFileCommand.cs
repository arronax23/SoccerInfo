using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristicsFromFile;
public class SavePlayersCharacteristicsFromFileCommand : ICommand
{
    public string FileName { get; set; } = null!;
}

