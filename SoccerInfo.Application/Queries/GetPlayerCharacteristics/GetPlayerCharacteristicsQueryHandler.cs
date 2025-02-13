using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Application.Queries.GetPlayerCharacteristics;

internal class GetPlayerCharacteristicsQueryHandler(
    ApplicationDbContext dbContext, 
    IMapper mapper) : IQueryHandler<GetPlayerCharacteristicsQuery, PlayerCharacteristicsDto>
{
    public Task<PlayerCharacteristicsDto> Handle(GetPlayerCharacteristicsQuery request, CancellationToken cancellationToken)
    {
        var isGoalkeeper = dbContext.Players
            .Single(x => x.Id == request.PlayerId)
            .Position == "Goalkeeper";

        PlayerCharacteristic characteristic = null!;

        if (isGoalkeeper)
            characteristic = dbContext.PlayerCharacteristics.Single(x => x.PlayerId == request.PlayerId);
        else
            characteristic = dbContext.PlayerCharacteristics.Single(x => x.PlayerId == request.PlayerId);


        var dto = mapper.Map<PlayerCharacteristicsDto>(characteristic);

        dto.IsGoalkeeper = isGoalkeeper;
        dto.BrithPlace!.CountryBase64Image = GetCountryBase64Image(dto.BrithPlace.Country);

        if (dto.NationalTeam != null)
            dto.NationalTeam.CountryBase64Image = GetCountryBase64Image(dto.NationalTeam.Country);

        return Task.FromResult(dto);
    }


    private string? GetCountryBase64Image(string? countryName)
    {
        return dbContext.Nationalities
            .SingleOrDefault(x => x.Country == countryName)?
            .CountryFlag!.ImageSvgBase64;
    }
}
