using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.API.Controllers;
internal class SearchPlayerQuery : IQuery<PlayerDto?>
{
    public int PlayerId { get; set; }
}