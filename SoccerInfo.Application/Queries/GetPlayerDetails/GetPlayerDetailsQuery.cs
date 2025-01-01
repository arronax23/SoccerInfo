using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayerDetails;

public class GetPlayerDetailsQuery : IQuery<PlayerDetailsDto>
{
    public int PlayerId { get; set; }
}

