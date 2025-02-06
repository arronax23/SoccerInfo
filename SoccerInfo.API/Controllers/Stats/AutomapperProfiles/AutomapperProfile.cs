
using AutoMapper;
using SoccerInfo.API.Controllers.Stats.Requests;
using static SoccerInfo.Application.Queries.GetPlayersByStats.GetPlayersByStatsQuery;

namespace SoccerInfo.API.Controllers.Stats.AutomapperProfiles;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<PlayerStatsFilterRequest, PlayerStatsFilterDto>();
    }
}
