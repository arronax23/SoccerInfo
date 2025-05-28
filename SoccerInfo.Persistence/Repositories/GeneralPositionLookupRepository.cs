using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;

namespace SoccerInfo.Persistence.Repositories;
internal class GeneralPositionLookupRepository(ApplicationDbContext dbContext) : IGeneralPositionLookupRepository
{
    public GeneralPosition_Lookup Get(GeneralPosition generalPosition)
    {
        return dbContext.GeneralPositions_Lookup.Single(gp => gp.Name == generalPosition.ToString());
    }
}
