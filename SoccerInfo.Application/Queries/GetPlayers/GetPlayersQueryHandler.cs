using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayers;

internal class GetPlayersQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetPlayersQuery, IEnumerable<PlayerDto>>
{
    public Task<IEnumerable<PlayerDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQueryRaw<PlayerData>(
                $@"SELECT 
                        p.Id,
                        p.Name,
                        p.Position,
                        p.FaceImageBase64,
                        p.Age, p.DateOfBirth, 
                        p.MarketValue, p.MarketValueUnit, 
                        ni.Base64Image as NationalityImage 
                    FROM Players p
                    JOIN NationalityImagePlayer nip ON nip.PlayerId = p.Id
                    JOIN NationalityImages ni ON nip.NationalityImageId = ni.Id
                    WHERE p.TeamId = {request.TeamId}")
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
            }));
    }


    private class PlayerData
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
