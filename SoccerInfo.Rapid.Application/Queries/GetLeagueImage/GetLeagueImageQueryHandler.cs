using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

//namespace SoccerInfo.Rapid.Application.Queries.GetLeague;
//internal class GetLeagueImageQueryHandler(IGenericRepository<League> repository) : IQueryHandler<GetLeagueImageQuery, string>
//{
//    public Task<string> Handle(GetLeagueImageQuery request, CancellationToken cancellationToken)
//    {
//        var league = repository.ToQuery().SingleOrDefault(l => l.Id == request.LeagueId);

//        return Task.FromResult(league.LeagueImageBase64!);
//    }
//}
