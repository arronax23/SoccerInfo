using Microsoft.EntityFrameworkCore;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayersGroupDto;

namespace SoccerInfo.Application.Queries.GetPlayers;

internal class GetPlayersQueryHandler(ApplicationDbContext dbContext) : IQueryHandler<GetPlayersQuery, IEnumerable<PlayersGroupDto>>
{
    public Task<IEnumerable<PlayersGroupDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(dbContext.Players
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
                    FaceImageBase64 = p.FaceImageBase64,
                    MarketValue = p.MarketValue,
                    MarketValueUnit = p.MarketValueUnit,
                    NationalityImageBase64Collection = p.Nationalities.Select(n => n.CountryFlag!.ImageSvgBase64),
                })
            })
            .AsEnumerable());
    }
}
