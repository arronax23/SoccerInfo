using AutoMapper;
using SoccerInfo.Domain.Models;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;


namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
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

        CreateMap<League, League>()
            .ForMember(x => x.Teams, opt => opt.Ignore());

        CreateMap<Team, Team>()
            .ForMember(x => x.Players, opt => opt.Ignore());

        CreateMap<Player, Player>()
            .ForMember(x => x.Nationalities, opt => opt.Ignore());
    }
}