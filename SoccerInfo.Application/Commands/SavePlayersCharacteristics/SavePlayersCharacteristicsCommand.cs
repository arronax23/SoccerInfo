using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;
public class SavePlayersCharacteristicsCommand : ICommand
{
    public string FileName { get; set; } = null!;
}

