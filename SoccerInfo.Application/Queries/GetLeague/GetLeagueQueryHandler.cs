using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetLeague;

internal class GetLeagueQueryHandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetLeagueQuery, LeagueDto>
{
    public async Task<LeagueDto> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor
                .SqlQuerySingleAsync<LeagueDto>(
                    @$"SELECT Id, Name, LeagueImageBase64, CountryFlagBase64 FROM Leagues WHERE Id = @LeagueId",
                    new { LeagueId = request.LeagueId });
    }
}
