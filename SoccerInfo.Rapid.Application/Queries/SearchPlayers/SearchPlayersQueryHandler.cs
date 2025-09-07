using SoccerInfo.Application.Abstractions.Interfaces;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Utilities;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.SearchPlayers;
internal class SearchPlayersQueryHandler(
    ISqlExecutor sqlExecutor, 
    IImageUrlGenerator imageUrlGenerator) : IQueryHandler<SearchPlayersQuery, IEnumerable<PlayerOverviewDto>?>
{
    public async Task<IEnumerable<PlayerOverviewDto>?> Handle(SearchPlayersQuery request, CancellationToken cancellationToken)
    {
       var rawData = (
            await sqlExecutor
                   .SqlQueryAsync<PlayerOverviewData>(
                    $@"
                    SELECT 
                        p.Id, 
                        p.Name,
                        p.Age,
                        t.Id as TeamId, 
                        t.Name as TeamName, 
                        l.Id as LeagueId,
                        l.Name as LeagueName,
                        il.Id as LeagueLogoId,
                        it.Id as TeamLogoId, 
                        ip.Id as FaceImageId
                    FROM Players p
                    JOIN Teams t ON t.Id = p.TeamId 
                    JOIN Leagues l ON l.Id = t.LeagueId
                    LEFT JOIN Images il ON il.Id = l.LogoId
                    LEFT JOIN Images it ON it.Id = t.LogoId
                    LEFT JOIN Images ip ON ip.Id = p.FaceImageId
                    WHERE p.Name COLLATE Latin1_general_CI_AI
                    LIKE @KeywordPhrase COLLATE Latin1_general_CI_AI", new { KeywordPhrase = $"%{request.Keyword}%" }))
                   .Skip((request.PageNumber - 1) * request.PageSize)
                   .Take(request.PageSize);


        return rawData.Select(p => new PlayerOverviewDto()
        {
            Id = p.Id,
            FaceImageUrl = imageUrlGenerator.Generate(p.FaceImageId),
            Name = p.Name,
            Age = p.Age,
            Team = new PlayerOverviewDto.TeamDto()
            {
                Id = p.TeamId,
                Name = p.TeamName,
                LogoUrl = imageUrlGenerator.Generate(p.TeamLogoId)
            },
            League = new PlayerOverviewDto.LeagueDto()
            {
                Id = p.LeagueId,
                Name = p.LeagueName,
                LogoUrl = imageUrlGenerator.Generate(p.LeagueLogoId)
            }
        });
    }


    private class PlayerOverviewData
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int FaceImageId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public int TeamLogoId { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = null!;
        public int LeagueLogoId { get; set; }
    }
}
