using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfoFromFile;

public class SavePlayersGeneralInfoFromFileCommand : ICommand
{
    public string FileName { get; set; } = null!;
}
