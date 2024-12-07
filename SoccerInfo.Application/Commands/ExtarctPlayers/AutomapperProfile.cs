using AutoMapper;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.Extractor.Dto.ExtractionDto;


namespace SoccerInfo.Application.Commands.ExtarctPlayers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<LeagueDto, League>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<TeamDto, Team>()
            .ForMember(x => x.TeamImageBase64, opt => opt.MapFrom(z => z.TeamImageBase64))
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.LeagueId, opt => opt.Ignore());

        CreateMap<PlayerDto, Player>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.TeamId, opt => opt.Ignore());

        CreateMap<NationalityImageDto, NationalityImage>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.PlayerId, opt => opt.Ignore());
    }
}