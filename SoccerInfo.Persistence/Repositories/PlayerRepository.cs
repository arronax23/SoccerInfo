using Microsoft.EntityFrameworkCore;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Repositories;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Repositories.Generic;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Repositories;

internal class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    private readonly ApplicationDbContext _context;
    public PlayerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    [Obsolete]
    public async Task<Player?> FindMatchingAsync(Player reference)
    {
        return await _context.Players
            .SingleOrDefaultAsync(BuildAccentInsensitivePredicate(reference));
    }

    [Obsolete]
    private static Expression<Func<Player, bool>> BuildAccentInsensitivePredicate(Player other)
    {
        return p =>
            EF.Functions.Collate(p.Name, "Latin1_General_CI_AI") == other.Name &&
            p.DateOfBirth == other.DateOfBirth;
    }

    public async Task<int> GetPlayersCount()
    {
        return await _context.Players.CountAsync();
    }

    public async Task<int> GetPlayersWithoutCharacteristicsCount
()
    {
        return await _context.Players.Where(p => p.Characteristics == null).CountAsync();
    }

}