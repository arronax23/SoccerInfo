using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;
internal class GetLeagueQueryHandler(IGenericRepository<League> repository) : IQueryHandler<GetLeagueQuery, LeagueDto>
{
    public Task<LeagueDto> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        var league = repository.ToQuery().SingleOrDefault(l => l.Id == request.LeagueId);

        return Task.FromResult(new LeagueDto()
        {
            Id = league.Id,
            Name = league.Name
        });
    }
}
