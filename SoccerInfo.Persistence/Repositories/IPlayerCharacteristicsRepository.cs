namespace SoccerInfo.Persistence.Repositories;

public interface IPlayerCharacteristicsRepository
{
    Task<int> GetCharacteristicsCount();
}

