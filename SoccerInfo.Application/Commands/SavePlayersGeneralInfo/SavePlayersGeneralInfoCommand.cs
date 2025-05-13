using SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;

public class SavePlayersGeneralInfoCommand : ICommand
{
    public GeneralInfoExtractionData Extraction { get; set; } = null!;
}
