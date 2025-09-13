using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtractPlayersCharacteristics;
public class ExtractPlayersCharacteristicsCommand : ICommand<PlayersCharacteristicsExtractionData?>
{
    public IEnumerable<int> PlayersIds { get; set; } = null!;
}

