using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Databases
{
  public class ApplicationDbContext : IdentityDbContext<AppUser>
  {
    #region DbSet
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    #endregion
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Employee>(options =>
      {
        options
          .HasOne(d => d.Department)
          .WithMany(d => d.Members)
          .HasForeignKey(e => e.DepartmentId);
      });

      builder.Entity<Department>(options =>
      {
        options
          .HasMany(d => d.Members)
          .WithOne(e => e.Department);
      });

      base.OnModelCreating(builder);
      builder.Entity<IdentityRole>().HasData(
        new() { Name = "Admin", NormalizedName = "ADMIN" },
        new() { Name = "Manager", NormalizedName = "MANAGER" },
        new() { Name = "Employee", NormalizedName = "EMPLOYEE" },
        new() { Name = "Client", NormalizedName = "CLIENT" }
      );
    }
  }
}