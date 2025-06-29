using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayerCharacteristics;

public class GetPlayerCharacteristicsQuery : IQuery<PlayerCharacteristicsDto?>
{
    public int PlayerId { get; set; }
}

