using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayers;

internal class GetPlayersQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetPlayersQuery, IEnumerable<PlayerDto>>
{
    public async Task<IEnumerable<PlayerDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        return (await
            sqlExecutor
            .SqlQueryAsync<PlayerModel>(
                $@"SELECT 
                    p.Id,
                    p.Name,
                    p.Position,
                    p.FaceImageBase64,
                    p.Age, p.DateOfBirth, 
                    p.MarketValue, p.MarketValueUnit, 
                    cf.ImageSvgBase64 as NationalityImage 
                FROM Players p
                JOIN NationalityPlayer np ON np.PlayerId = p.Id
                JOIN Nationalities ni ON np.NationalityId = ni.Id
                JOIN CountryFlags_Lookup cf ON ni.CountryFlagId = cf.Id
                WHERE p.TeamId = @TeamId", new { TeamId = request.TeamId }))
            .ToList()
            .GroupBy(x => x.Id)
            .Select(y => new PlayerDto()
            {
                Id = y.Key,
                Name = y.First().Name,
                Position = y.First().Position,
                Age = y.First().Age,
                MarketValue = y.First().MarketValue,
                MarketValueUnit = y.First().MarketValueUnit,
                FaceImageBase64 = y.First().FaceImageBase64,
                DateOfBirth = y.First().DateOfBirth,
                NationalityImageBase64Collection = y.Select(z => z.NationalityImage),
            });
    }
    private class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int Age { get; set; }
        public float? MarketValue { get; set; }
        public string? MarketValueUnit { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? FaceImageBase64 { get; set; }
        public string? NationalityImage { get; set; }
    }
}
