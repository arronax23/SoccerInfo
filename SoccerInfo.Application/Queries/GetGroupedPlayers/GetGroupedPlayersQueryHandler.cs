using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayersGroupDto;

namespace SoccerInfo.Application.Queries.GetGroupedPlayers;

internal class GetGroupedPlayersQueryHandler(IPlayerRepository playerRepository) : IQueryHandler<GetGroupedPlayersQuery, IEnumerable<PlayersGroupDto>>
{
    public Task<IEnumerable<PlayersGroupDto>> Handle(GetGroupedPlayersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(playerRepository
            .ToQuery()
            .AsNoTracking()
            .Where(p => p.TeamId == request.TeamId)
            .GroupBy(p => new { p.GeneralPositionId, p.GeneralPosition.DisplayName })
            .OrderBy(gp => gp.Key.GeneralPositionId)
            .Select(gp => new PlayersGroupDto()
            {
                DisplayName = gp.Key.DisplayName,
                Players = gp.Select(p => new PlayerDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    DateOfBirth = p.DateOfBirth,
                    Position = p.Position,
                    FaceImageBase64 = p.FaceImage.Base64,
                    FaceImageMimeType = p.FaceImage.MimeType,
                    MarketValue = p.MarketValue,
                    MarketValueUnit = p.MarketValueUnit,
                    NationalityImageBase64Collection = p.Nationalities.Select(n => n.CountryFlag!.ImageSvgBase64),
                })
            })
            .AsEnumerable());
    }
}
