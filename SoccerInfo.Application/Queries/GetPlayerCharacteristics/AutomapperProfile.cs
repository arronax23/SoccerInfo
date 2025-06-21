using AutoMapper;
using SoccerInfo.Application.Queries.Dtos;
using SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Shared.Utilities;
using static SoccerInfo.Application.Queries.Dtos.PlayerCharacteristicsDto;

namespace SoccerInfo.Application.Queries.GetPlayerCharacteristics;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<PlayerCharacteristic, PlayerCharacteristicsDto>()
            .ForMember(x => x.LeadingFoot, opt => opt.MapFrom(y => y.LeadingFoot.CapitalizeFirstLetter()));        
        CreateMap<NationalTeam, NationalTeamDto>();        
        CreateMap<BrithPlace, BrithPlaceDto>();        
        CreateMap<SocialMedia, SocialMediaDto>();        
        CreateMap<GoalKeeperStats, GoalKeeperStatsDto>();        
        CreateMap<OutfieldPlayerStats, OutfieldPlayerStatsDto>();        
        CreateMap<StatsLeague, StatsLeagueDto>();        
    }
}
