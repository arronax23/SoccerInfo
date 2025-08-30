using SoccerInfo.Domain.Lookups.GeneralPosition;

namespace SoccerInfo.Domain.Repositories;

public interface IGeneralPositionLookupRepository
{
    Task<GeneralPosition_Lookup> Get(GeneralPosition generalPosition);
}

