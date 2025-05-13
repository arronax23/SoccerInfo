using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;

public class SavePlayersCharacteristicsCommand : ICommand
{
    public PlayersCharacteristicsExtractionData Extraction { get; set; } = null!;
}

