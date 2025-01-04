using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.Application.Queries.Dtos.PlayerDetailsDto;

namespace SoccerInfo.Application.Queries.GetPlayerDetails;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Player, PlayerDetailsDto>()
            .ForMember(x => x.MarketValueChanges, opt => opt.Ignore());

        CreateMap<Nationality, NationalityDto>()
            .ForMember(x => x.CountryFlagBase64Image, opt => opt.MapFrom(y => y.CountryFlag!.ImageSvgBase64));
    }
}
