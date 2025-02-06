using AutoMapper;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.Stats;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
            .ForMember(dest => dest.LeagueId, opt => opt.MapFrom(src => src.Team.LeagueId));
            //.ForMember(dest => dest.TeamImageBase64, opt => opt.MapFrom(src => src.Team.TeamImageBase64))
            //.ForMember(dest => dest.LeagueImageBase64, opt => opt.MapFrom(src => src.Team.League!.LeagueImageBase64));

        CreateMap<Player, StatsPlayerMarketValueDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.MarketValue, opt => opt.MapFrom(src => src.Stats.MarketValue))
            .ForMember(dest => dest.MarketValueUnit, opt => opt.MapFrom(src => src.Stats.MarketValueUnit));

        CreateMap<Player, StatsPlayerGoalsDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Stats.TotalGoals));

        CreateMap<Player, StatsPlayerAssistsDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.Assists, opt => opt.MapFrom(src => src.Stats.TotalAssists));

        CreateMap<Player, StatsPlayerGoalsAndAssistsDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.GoalsAndAssists, opt => opt.MapFrom(src => src.Stats.TotalGoalsAndAssists))
            .ForMember(dest => dest.Goals, opt => opt.MapFrom(src => src.Stats.TotalGoals))
            .ForMember(dest => dest.Assists, opt => opt.MapFrom(src => src.Stats.TotalAssists));

        CreateMap<Player, StatsPlayerCleanSheetsDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.CleanSheets, opt => opt.MapFrom(src => src.Stats.TotalCleanSheets));

        CreateMap<Player, StatsPlayerGoalsConcededDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.GoalsConceded, opt => opt.MapFrom(src => src.Stats.TotalGoalsConceded));

        CreateMap<Player, StatsPlayerHeightDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Stats.Height));

        CreateMap<DateRange, DateRangeDto>();

        CreateMap<Player, StatsPlayerAgeDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Stats.Age));

        CreateMap<Player, StatsPlayerContractExpirationDto>()
            .IncludeBase<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.ContractPeriod,opt => opt.MapFrom(src => src.Stats.ContractPeriod));   
    }
}
