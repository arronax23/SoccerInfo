using Microsoft.EntityFrameworkCore;
using SoccerInfo.Domain.Models;
using SoccerInfo.Domain.Models.Abstractions;
using SoccerInfo.Domain.Models.Extraction;
using SoccerInfo.Domain.Models.GeneralPosition;
using SoccerInfo.Domain.Models.PlayerCharacteristicsAggregate;
using SoccerInfo.Domain.Models.Stats;
using SoccerInfo.Domain.Models.Transfers;
using static SoccerInfo.Domain.Models.Transfers.Transfer;

namespace SoccerInfo.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<ClubOverview> ClubsOverviews { get; set; }
    public DbSet<ClubInfo> ClubsInfos { get; set; }
    public DbSet<MarketValueChange> MarketValueChanges { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    public DbSet<PlayerCharacteristic> PlayerCharacteristics { get; set; }
    public DbSet<StatsLeague> StatsLeagues { get; set; }
    public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    public DbSet<CountryFlag_Lookup> CountryFlags_Lookup { get; set; }
    public DbSet<GeneralPosition_Lookup> GeneralPositions_Lookup { get; set; }
    public DbSet<LeagueLinkLookup> LeagueLinksLookup { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AuditEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        AuditEntities();
        return base.SaveChanges();
    }

    private void AuditEntities()
    {
        var now = DateTime.Now;

        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is IAuditable))
        {
            if (entry.State == EntityState.Added)
            {
                ((IAuditable)entry.Entity).CreatedDate = now;
                ((IAuditable)entry.Entity).LastUpdatedDate = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                ((IAuditable)entry.Entity).LastUpdatedDate = now;
            }
        }
    }
}
