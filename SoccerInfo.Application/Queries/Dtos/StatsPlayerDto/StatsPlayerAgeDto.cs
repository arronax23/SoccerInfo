using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerAgeDto : StatsPlayerBaseDto
{
    public DateRangeDto Age { get; set; } = null!;
}

