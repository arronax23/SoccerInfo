using AutoMapper;
using SoccerInfo.Domain.Models;


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