using AutoMapper;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using static SoccerInfo.FrontendScraper.ScrapePlayersCharacterstics.Dto.PlayersCharacteristicsExtractionData;

namespace SoccerInfo.Application.Commands.SavePlayersCharacteristics;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<SocialMediaData, SocialMedia>();

        CreateMap<GoalKeeperStatsData, GoalKeeperStats>()
            .ForMember(x => x.League, 
                opt => opt.MapFrom(y => new StatsLeague() { Name = y.League, Base64Image = y.LeagueBase64Image}));

        CreateMap<OutfieldPlayerStatsData, OutfieldPlayerStats>()
            .ForMember(x => x.League,
                opt => opt.MapFrom(y => new StatsLeague() { Name = y.League, Base64Image = y.LeagueBase64Image }));
    }
}
