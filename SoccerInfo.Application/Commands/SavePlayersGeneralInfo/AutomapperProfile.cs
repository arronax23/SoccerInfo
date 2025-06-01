using AutoMapper;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.FrontendScraper.ScrapePlayersGeneralInfo.Dto.GeneralInfoExtractionData;


namespace SoccerInfo.Application.Commands.SavePlayersGeneralInfo;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<League, League>()
            .ForMember(x => x.Teams, opt => opt.Ignore());

        CreateMap<Team, Team>()
            .ForMember(x => x.Players, opt => opt.Ignore());

        CreateMap<Player, Player>()
            .ForMember(x => x.Nationalities, opt => opt.Ignore());
    }
}