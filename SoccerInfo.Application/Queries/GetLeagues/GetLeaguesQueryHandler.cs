using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeagues;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeaguesQuery, IEnumerable<LeagueDto>>
{
    public async Task<IEnumerable<LeagueDto>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor.SqlQueryAsync<LeagueDto>(@"SELECT Id, Name, LeagueImageBase64, CountryFlagBase64 FROM Leagues");
    }
}
