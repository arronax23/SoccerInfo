
using AutoMapper;
using SoccerInfo.API.Controllers.Stats.Requests;
using SoccerInfo.Application.Queries.GetPlayersByStatsFilter;

namespace SoccerInfo.API.Controllers.Stats.AutomapperProfiles;
internal class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<PlayerStatsFilterRequest, GetPlayersByStatsFilterQuery>();
    }
}
