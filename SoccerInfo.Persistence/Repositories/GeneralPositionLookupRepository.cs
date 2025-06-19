using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;

namespace SoccerInfo.Persistence.Repositories;
internal class GeneralPositionLookupRepository(ApplicationDbContext dbContext) : IGeneralPositionLookupRepository
{
    public async Task<GeneralPosition_Lookup> Get(GeneralPosition generalPosition)
    {
        return await dbContext.GeneralPositions_Lookup.SingleAsync(gp => gp.Name == generalPosition.ToString());
    }
}
