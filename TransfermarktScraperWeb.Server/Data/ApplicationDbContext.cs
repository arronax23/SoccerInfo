using Microsoft.EntityFrameworkCore;
using SoccerInfo.Infrastructure.Data.Models; 

namespace SoccerInfoWeb.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NationalityImage> NationalityImages { get; set; }

}
