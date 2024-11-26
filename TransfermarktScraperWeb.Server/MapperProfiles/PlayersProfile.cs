using AutoMapper;
using TransfermarktScraper.Dto;
using TransfermarktScraperWeb.Server.Data.Models;

namespace TransfermarktScraperWeb.Server.MapperProfiles;

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
