using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;

public class GetLeagueImageQuery : IQuery<ImageDto?>
{
    public int ImageId { get; set; }
}
