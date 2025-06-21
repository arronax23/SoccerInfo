using SoccerInfo.Persistence.Data.Models;

namespace SoccerInfo.Persistence.Repositories;

public interface IPlayerRepository
{
    Task<Player?> FindMatchingAsync(Player reference);
}

