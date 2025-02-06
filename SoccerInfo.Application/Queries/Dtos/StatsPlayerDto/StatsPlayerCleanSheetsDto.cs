using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;

namespace SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;

public class StatsPlayerCleanSheetsDto : StatsPlayerBaseDto
{
    public int CleanSheets { get; set; }
}

