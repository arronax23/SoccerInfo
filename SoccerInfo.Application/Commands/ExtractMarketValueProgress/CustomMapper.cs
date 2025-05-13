using AutoMapper;
using SoccerInfo.Persistence.Data.Models;
using static SoccerInfo.BackendScraper.MarketValueProgressScraper;
using static SoccerInfo.BackendScraper.MarketValueProgressScraper.MarketValueProgressData;

namespace SoccerInfo.Application.Commands.ExtractMarketValueProgress;
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<ProgressItem, MarketValueChange>();
    }
}

public class CustomMapper(IMapper mapper)
{
    public IEnumerable<MarketValueChange> Map(MarketValueProgressData marketValueProgressData)
    {
        var marketValueChanges = mapper.Map<IEnumerable<MarketValueChange>>(marketValueProgressData.ProgressCollection);

        foreach (var marketValueChange in marketValueChanges)
            marketValueChange.PlayerTransferMarktId = marketValueProgressData.PlayerTransfermarktId;

        return marketValueChanges;
    }
}

