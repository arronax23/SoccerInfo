using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data;

namespace SoccerInfo.Persistence.Repositories;
internal class PlayerCharacteristicsRepository(ApplicationDbContext dbContext) : IPlayerCharacteristicsRepository
{
    public async Task<int> GetCharacteristicsCount()
    {
        return await dbContext.PlayerCharacteristics.CountAsync();
    }
}
