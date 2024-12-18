using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeague;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeagueQuery, LeagueDto>
{
    public Task<LeagueDto> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            sqlExecutor
            .SqlQueryRaw<LeagueDto>(
                @$"SELECT Id, Name, LeagueImageBase64, CountryFlagBase64 FROM Leagues WHERE Id = {request.LeagueId}")
            .Single());
    }
}
