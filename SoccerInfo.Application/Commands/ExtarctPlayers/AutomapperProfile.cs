using AutoMapper;
using SoccerInfo.Extractor.Dto;
using SoccerInfo.Persistence.Data.Models;


namespace SoccerInfo.Application.Commands.ExtarctPlayers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<string, NationalityImage>()
            .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src));

        CreateMap<ExtractionDto.PlayerDto, Player>()
            .ForMember(dest => dest.NationalityImageBase64Collection, opt => opt.MapFrom(src => src.NationalityImageBase64Collection));

    }
}