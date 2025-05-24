using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data.Models;
using SoccerInfo.Persistence.Data.Models.GeneralPosition;
using SoccerInfo.Persistence.Data.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Persistence.Data.Models.Stats;

namespace SoccerInfo.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MarketValueChange> MarketValueChanges { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    public DbSet<PlayerCharacteristic> PlayerCharacteristics { get; set; }
    public DbSet<StatsLeague> StatsLeagues { get; set; }
    public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    public DbSet<CountryFlag_Lookup> CountryFlags_Lookup { get; set; }
    public DbSet<GeneralPosition_Lookup> GeneralPositions_Lookup { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}
