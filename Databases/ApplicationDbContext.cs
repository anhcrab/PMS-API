using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Databases
{
  public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
  {
    #region DbSet
    // public DbSet<Employee> Employees { get; set; }
    // public DbSet<Department> Departments { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
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