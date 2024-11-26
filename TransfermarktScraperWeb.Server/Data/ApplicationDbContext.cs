using Microsoft.EntityFrameworkCore;
using TransfermarktScraperWeb.Server.Data.Models;

namespace TransfermarktScraperWeb.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NationalityImage> NationalityImages { get; set; }

}
