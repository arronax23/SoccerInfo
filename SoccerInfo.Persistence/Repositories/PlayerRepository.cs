using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.Data.Models;
using System.Linq.Expressions;

namespace SoccerInfo.Persistence.Repositories;

internal class PlayerRepository(ApplicationDbContext dbContext) : IPlayerRepository
{
    public async Task<Player?> FindMatchingAsync(Player reference)
    {
        return await dbContext.Players
            .SingleOrDefaultAsync(BuildAccentInsensitivePredicate(reference));
    }

    private static Expression<Func<Player, bool>> BuildAccentInsensitivePredicate(Player other)
    {
        return p =>
            EF.Functions.Collate(p.Name, "Latin1_General_CI_AI") == other.Name &&
            p.DateOfBirth == other.DateOfBirth;
    }
}