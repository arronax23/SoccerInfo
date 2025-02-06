using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;
public class StatsPlayerMarketValueDto : StatsPlayerBaseDto
{
    public float MarketValue {  get; set; }
    public string MarketValueUnit { get; set; } = null!;
}

