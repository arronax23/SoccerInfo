using SoccerInfo.Domain.Lookups;
namespace SoccerInfo.Domain.Repositories;

public interface IDummyImageRepository
{
    Task<DummyImageLookup> Get(DummyImageLookup.EntityType generalPosition);
}

