using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;
public class StatsPlayerMarketValueProgressDto : StatsPlayerBaseDto
{
    public float MarketValueChange { get; set; }
    public string MarketValueUnit { get; set; } = null!;
}

