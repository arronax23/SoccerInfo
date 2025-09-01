using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models;
using static SoccerInfo.Application.Queries.Dtos.PlayerDetailsDto;

namespace SoccerInfo.Application.Queries.GetPlayerDetails;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Player, PlayerDetailsDto>()
            .ForMember(x => x.MarketValueChanges, opt => opt.Ignore());

        CreateMap<Nationality, NationalityDto>()
            .ForMember(x => x.Country, opt => opt.MapFrom(y => y.Country_Lookup))
            .ForMember(x => x.CountryFlag, opt => opt.MapFrom(y => y.CountryFlag!.Image));

        CreateMap<Image, ImageDto>();

    }
}
