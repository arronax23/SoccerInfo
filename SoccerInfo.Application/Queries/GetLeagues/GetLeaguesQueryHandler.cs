using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeagues;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeaguesQuery, IEnumerable<LeagueDto>>
{
    public Task<IEnumerable<LeagueDto>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQueryRaw<LeagueDto>(@"SELECT Id, Name, LeagueImageBase64, CountryFlagBase64 FROM Leagues")
            .AsEnumerable());
    }
}
