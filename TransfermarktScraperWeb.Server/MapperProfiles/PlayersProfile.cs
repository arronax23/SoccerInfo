using AutoMapper;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Infrastructure.Data.Models;


namespace SoccerInfo.Infrastructure.MapperProfiles;

public class PlayersProfile :  Profile
{
    public PlayersProfile()
    {
        CreateMap<string, NationalityImage>()
            .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src));

        CreateMap<PlayersExtractionDto.PlayerDto, Player>()
            .ForMember(dest => dest.NationalityImageBase64Collection, opt => opt.MapFrom(src => src.NationalityImageBase64Collection));

    }
}
