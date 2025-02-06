using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerGoalsConcededDto : StatsPlayerBaseDto
{
    public int GoalsConceded { get; set; }
}

