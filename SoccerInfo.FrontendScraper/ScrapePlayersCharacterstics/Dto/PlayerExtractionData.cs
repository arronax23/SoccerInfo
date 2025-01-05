namespace SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto;
public class PlayerExtractionData
{
    public int TransfermarktId { get; set; }
    public string TransfermarktURL { get; set; } = null!;
    public bool isGoalkeeper { get; set; }
}
