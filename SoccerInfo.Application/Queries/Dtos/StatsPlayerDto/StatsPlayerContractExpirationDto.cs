using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerContractExpirationDto : StatsPlayerBaseDto
{
    public DateRangeDto ContractPeriod { get; set; } = null!;

}
