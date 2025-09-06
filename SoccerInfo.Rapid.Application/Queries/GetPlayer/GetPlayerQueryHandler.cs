using SoccerInfo.Domain.Repositories;
using SoccerInfo.Rapid.Application.Queries.Dtos;
using SoccerInfo.Rapid.Application.Utilities;
using SoccerInfo.Shared.CQRS;

namespace SoccerInfo.Rapid.Application.Queries.GetLeague;
internal class GetPlayerQueryHandler(
    IPlayerRepository playerRepository, 
    IImageUrlGenerator imageUrlGenerator) : IQueryHandler<GetPlayerQuery, PlayerDto?>
{
    public async Task<PlayerDto?> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = playerRepository.ToQuery().SingleOrDefault(p => request.PlayerId == p.Id);

        if (player == null) 
            return null;

        return new PlayerDto()
        {
            Id = player.Id,
            Age = player.Age,
            MarketValue = $"{player.MarketValue}{player.MarketValueUnit}€",
            Name = player.Name,
            Team = new PlayerDto.TeamDto()
            {
                Id = player.Team.Id,
                Name = player.Team.Name,
                LogoUrl = imageUrlGenerator.Generate(player.Team.LogoId!.Value)
            },
            Nationalities = player.Nationalities.Select(n => new NationalityDto()
            {
                Country = n.Country,
                CountryImageUrl = imageUrlGenerator.Generate(n.CountryFlag!.ImageId)
            })
        };
    }
}
