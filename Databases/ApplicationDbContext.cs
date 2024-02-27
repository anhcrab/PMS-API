using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Databases
{
  public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
  {
    #region DbSet
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AppUser>(opts =>
      {
        opts
          .HasMany(u => u.Projects)
          .WithOne(p => p.Responsible)
          .HasForeignKey(p => p.ResponsibleId);
      });
      builder.Entity<ProjectType>(opts =>
      {
        opts
          .HasMany(t => t.Projects)
          .WithOne(p => p.Type)
          .HasForeignKey(p => p.TypeId);
      });
      builder.Entity<Project>(opts =>
      {
        opts
          .HasOne(p => p.Responsible)
          .WithMany(u => u.Projects)
          .HasForeignKey(p => p.ResponsibleId);
        opts
          .HasOne(p => p.Type)
          .WithMany(t => t.Projects)
          .HasForeignKey(p => p.TypeId);
      });
      base.OnModelCreating(builder);
      builder.Entity<IdentityRole>().HasData(
        new() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Client", NormalizedName = "CLIENT", ConcurrencyStamp = Guid.NewGuid().ToString() }
      );
    }
  }
}