using Microsoft.EntityFrameworkCore;
using SoccerInfo.Domain.Lookups;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Persistence.Data;

namespace SoccerInfo.Persistence.Repositories;
internal class DummyImageRepository(ApplicationDbContext dbContext) : IDummyImageRepository
{
    public async Task<DummyImageLookup> Get(DummyImageLookup.EntityType type)
    {
        return await dbContext.DummyImagesLookup.SingleAsync(di => di.Type == type);
    }
}
