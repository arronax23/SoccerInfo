using SoccerInfo.Persistence.Data.Models.GeneralPosition;

namespace SoccerInfo.Persistence.Repositories;

public interface IGeneralPositionLookupRepository
{
    public GeneralPosition_Lookup Get(GeneralPosition generalPosition);
}

