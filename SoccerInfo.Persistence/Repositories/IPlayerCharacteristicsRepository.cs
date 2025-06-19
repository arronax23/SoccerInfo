namespace SoccerInfo.Persistence.Repositories;

public interface IPlayerCharacteristicsRepository
{
    public Task<int> GetCharacteristicsCount();
}

