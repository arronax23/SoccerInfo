using AutoMapper;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto;
using SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Stats;
using static SoccerInfo.Application.Queries.Dtos.StatsPlayerDto.Shared.StatsPlayerBaseDto;

namespace SoccerInfo.Application.Queries.GetPlayersByStats;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Player, StatsPlayerBaseDto>()
            .ForMember(dest => dest.PlayerId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FaceImageBase64, opt => opt.MapFrom(src => src.FaceImageBase64))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.Nationalities, opt =>
                opt.MapFrom(src => src.Nationalities.Select(n => new NationalityDto()
                {
                    ImageBase64 = n.CountryFlag!.ImageSvgBase64,
                    Name = n.Country_Lookup
                })))
            .ForMember(dest => dest.Team, opt => opt.MapFrom(src => new TeamDto()
            {
                Name = src.Team.Name,
                TeamImageBase64 = src.Team.TeamImageBase64,
                TeamId = src.TeamId
            }))
            .ForMember(dest => dest.League, opt => opt.MapFrom(src => new LeagueDto()
            {
                Name = src.Team.League!.Name,
                LeagueImageBase64 = src.Team.League!.LeagueImageBase64,
                LeagueId = src.Team.LeagueId!.Value
            }));

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
            .ForMember(dest => dest.ContractPeriod, opt => opt.MapFrom(src => src.Stats.ContractPeriod));   
    }
}
