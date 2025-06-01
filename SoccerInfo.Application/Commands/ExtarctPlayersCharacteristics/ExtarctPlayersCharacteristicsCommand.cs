using SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.ExtarctPlayersCharacteristics;
public class ExtarctPlayersCharacteristicsCommand : ICommand<PlayersCharacteristicsExtractionData?>
{
    public int PlayerCount { get; set; }
    public bool OnlyNewPlayers { get; set; }
}

