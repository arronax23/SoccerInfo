using Microsoft.EntityFrameworkCore;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Utilities;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;
internal class GetLeagueQueryHandler(
    IGenericRepository<League> repository, 
    IImageUrlGenerator imageUrlGenerator) : IQueryHandler<GetLeagueQuery, LeagueDto?>
{
    public async Task<LeagueDto?> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        var league = await repository.ToQuery().AsNoTracking().SingleOrDefaultAsync(l => l.Id == request.LeagueId);

        if (league is not null)
        {
            var imageUrl = league.LogoId != null ? imageUrlGenerator.Generate(league.LogoId.Value) : null;
            var countryFlagUrl = league.CountryFlagId != null ? imageUrlGenerator.Generate(league.CountryFlagId.Value) : null;

            return new LeagueDto()
            {
                Id = league.Id,
                Name = league.Name,
                LeagueImageUrl = imageUrl,
                CountryFlagUrl = countryFlagUrl
            };
        }
        else
            return null;
    }
}
