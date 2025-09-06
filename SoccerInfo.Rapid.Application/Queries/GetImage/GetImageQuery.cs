using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetImage;

public class GetImageQuery : IQuery<ImageDto?>
{
    public int ImageId { get; set; }
}
