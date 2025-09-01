using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Domain.Repositories.Generic;
using SoccerInfo.Shared.CQRS;
using static SoccerInfo.Application.Queries.Dtos.PlayerCharacteristicsDto;

namespace SoccerInfo.Application.Queries.GetPlayerCharacteristics;

internal class GetPlayerCharacteristicsQueryHandler(
    IPlayerRepository playerRepository,
    IGenericRepository<Nationality> nationalityRepository,
    IMapper mapper) : IQueryHandler<GetPlayerCharacteristicsQuery, PlayerCharacteristicsDto?>
{
    public Task<PlayerCharacteristicsDto?> Handle(GetPlayerCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        var player = playerRepository
            .ToQuery()
            .Single(x => x.Id == request.PlayerId);

        var isGoalkeeper = player.Position == "Goalkeeper";

        var dto = mapper.Map<PlayerCharacteristicsDto>(player.Characteristics);

        if (dto is not null)
        {
            dto.IsGoalkeeper = isGoalkeeper;
            dto.BrithPlace!.CountryImage = GetCountryImage(dto.BrithPlace.Country);

            if (dto.NationalTeam != null)
                dto.NationalTeam.CountryImage = GetCountryImage(dto.NationalTeam.Country);

            return Task.FromResult(dto)!;
        }
        else
            return Task.FromResult<PlayerCharacteristicsDto?>(null);
    }

    private ImageDto? GetCountryImage(string? countryName)
    {
        var img = nationalityRepository
            .ToQuery()
            .SingleOrDefault(x => x.Country == countryName)?
            .CountryFlag?.Image;

        if (img is null) 
            return null;

        return new ImageDto()
        {
            Base64 = img.Base64,
            MimeType = img.MimeType,
        };
    }
}
