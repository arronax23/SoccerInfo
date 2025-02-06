using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerGoalsAndAssistsDto : StatsPlayerBaseDto
{
    public int GoalsAndAssists { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
}
