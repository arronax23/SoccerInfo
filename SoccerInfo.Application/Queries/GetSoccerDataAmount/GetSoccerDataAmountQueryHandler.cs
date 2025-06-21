using SoccerInfo.Application.Interfaces;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetSoccerDataAmount;

internal class GetSoccerDataAmountQueryhandler(ISqlExecutor sqlExecutor) : IQueryHandler<GetSoccerDataAmountQuery, SoccerDataAmountDto>
{
    public async Task<SoccerDataAmountDto> Handle(GetSoccerDataAmountQuery request, CancellationToken cancellationToken)
    {
        return await
            sqlExecutor
                .SqlQuerySingleAsync<SoccerDataAmountDto>(
                    @$"SELECT 
                        (SELECT COUNT(*) FROM Leagues) AS LeaguesCount, 
                        (SELECT COUNT(*) FROM Teams) AS TeamsCount, 
                        (SELECT COUNT(*) FROM Players) AS PlayersCount");
    }
}
