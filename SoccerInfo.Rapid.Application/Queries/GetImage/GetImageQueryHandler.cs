using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetImage;
internal class GetImageQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetImageQuery, ImageDto?>
{
    public async Task<ImageDto?> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQuerySingleorDefaultAsync<ImageDto?>(
                    @"SELECT Base64, MimeType FROM Images
                      WHERE Id = @ImageId", new { ImageId = request.ImageId });

    }
}
