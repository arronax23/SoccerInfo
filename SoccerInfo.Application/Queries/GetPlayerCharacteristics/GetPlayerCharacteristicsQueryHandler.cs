using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayerCharacteristics;

internal class GetPlayerCharacteristicsQueryHandler(
    IPlayerRepository playerRepository,
    IGenericRepository<Nationality> nationalityRepository,
    IMapper mapper) : IQueryHandler<GetPlayerCharacteristicsQuery, PlayerCharacteristicsDto>
{
    public Task<PlayerCharacteristicsDto> Handle(GetPlayerCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        var player = playerRepository
            .ToQuery()
            .Single(x => x.Id == request.PlayerId);

        var isGoalkeeper = player.Position == "Goalkeeper";

        var dto = mapper.Map<PlayerCharacteristicsDto>(player.Characteristics);

        if (dto is not null)
        {
            dto.IsGoalkeeper = isGoalkeeper;
            dto.BrithPlace!.CountryBase64Image = GetCountryBase64Image(dto.BrithPlace.Country);

            if (dto.NationalTeam != null)
                dto.NationalTeam.CountryBase64Image = GetCountryBase64Image(dto.NationalTeam.Country);

            return Task.FromResult(dto);
        }
        else
            return Task.FromResult(new PlayerCharacteristicsDto());
    }

    private string? GetCountryBase64Image(string? countryName)
    {
        return nationalityRepository
            .ToQuery()
            .SingleOrDefault(x => x.Country == countryName)?
            .CountryFlag!.ImageSvgBase64;
    }
}
