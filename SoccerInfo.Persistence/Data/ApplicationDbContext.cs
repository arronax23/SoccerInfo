using Microsoft.EntityFrameworkCore;
using SoccerInfo.Persistence.Data.Models; 

namespace SoccerInfo.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NationalityImage> NationalityImages { get; set; }
}
