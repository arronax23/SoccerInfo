using AutoMapper;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.FrontendScraper.Dto.ExtractionData;


namespace SoccerInfo.Application.Commands.ExtarctPlayers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<LeagueData, League>()
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<TeamData, Team>()
            .ForMember(x => x.TeamImageBase64, opt => opt.MapFrom(z => z.TeamImageBase64))
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.LeagueId, opt => opt.Ignore());

        CreateMap<PlayerData, Player>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.TeamId, opt => opt.Ignore());

        CreateMap<NationalityData, Nationality>()
            .ForMember(x => x.Id, opt => opt.Ignore());
            ///.ForMember(x => x.Players, opt => opt.Ignore());
    }
}