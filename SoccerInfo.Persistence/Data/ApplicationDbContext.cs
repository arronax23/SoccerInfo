using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data.Models; 

namespace SoccerInfo.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MarketValueChange> MarketValueChanges { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    public DbSet<CountryFlag_Lookup> CountryFlags_Lookup { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }
}
