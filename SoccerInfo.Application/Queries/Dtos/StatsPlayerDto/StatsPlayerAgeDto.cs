using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using static SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.StatsPlayerContractExpirationDto;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerAgeDto : StatsPlayerBaseDto
{
    public DateRangeDto Age { get; set; } = null!;
}

