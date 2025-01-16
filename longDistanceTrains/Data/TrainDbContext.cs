using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using trains.Models;

namespace trains.Data;

public class TrainDbContext : IdentityDbContext
{
    public readonly IConfiguration Configuration;

    public TrainDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("PostgreConnect"));
    }
    
    public DbSet<Routes> routes { get; set; }
    public DbSet<Tickets> tickets { get; set; }
    public DbSet<AppUser> users { get; set; }
    public DbSet<Schedules> schedules { get; set; }
    public DbSet<Trains> trains { get; set; }
    public DbSet<Wagons> wagons { get; set; }
    public DbSet<Layouts> layouts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Layouts>()
            .HasKey(l => new { l.trainID, l.wagonID });

        modelBuilder.Entity<Layouts>()
            .HasOne(l => l.Train)
            .WithMany()
            .HasForeignKey(l => l.trainID);

        modelBuilder.Entity<Layouts>()
            .HasOne(l => l.Wagon)
            .WithMany()
            .HasForeignKey(l => l.wagonID);
    }
}