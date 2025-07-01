using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories.Generic;

namespace SoccerInfo.Domain.Repositories;

public interface IPlayerRepository : IGenericRepository<Player>
{
    Task<Player?> FindMatchingAsync(Player reference);
    Task<int> GetPlayersCount();
    Task<int> GetPlayersWithoutCharacteristicsCount();
}

