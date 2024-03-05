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
    public DbSet<WorkTask> WorkTasks { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<AppUser>(opts =>
      {
        opts
          .HasMany(u => u.Projects)
          .WithMany(p => p.Members);
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
          .HasOne(p => p.Type)
          .WithMany(t => t.Projects)
          .HasForeignKey(p => p.TypeId);
      });
      builder.Entity<WorkTask>(opts => 
      {
        opts
          .HasOne(w => w.Project)
          .WithMany(p => p.Tasks)
          .HasForeignKey(w => w.ProjectId);
        opts
          .HasOne(w => w.Member)
          .WithMany(e => e.Tasks)
          .HasForeignKey(w => w.MemberId);
      });
      base.OnModelCreating(builder);
      builder.Entity<IdentityRole>().HasData(
        new() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = Guid.NewGuid().ToString() },
        new() { Name = "Client", NormalizedName = "CLIENT", ConcurrencyStamp = Guid.NewGuid().ToString() }
      );

      // var pass = new PasswordHasher<AppUser>();
      // var admin = new AppUser
      // {
      //   UserName = "terus",
      //   Email = "phucthinhterus@gmail.com",
      //   FirstName = "Phúc Thịnh",
      //   LastName = "Phan",
      //   EmailConfirmed = true,
      //   NormalizedEmail = "PHUCTHINHTERUS@GMAIL.COM",
      //   NormalizedUserName = "TERUS",
      // };
      // admin.PasswordHash = pass.HashPassword(admin, "Terus@123");
      // List<AppUser> users = [];
      // users.Add(admin);
      // for (var i = 1; i <= 10; i++)
      // {
      //   var user = new AppUser
      //   {
      //     UserName = "user" + i,
      //     Email = "user" + i + "@gmail.com",
      //     FirstName = i.ToString(),
      //     LastName = "User",
      //     EmailConfirmed = true,
      //     NormalizedEmail = "USER" + i + "@GMAIL.COM",
      //     NormalizedUserName = "USER" + i,
      //   };
      //   user.PasswordHash = pass.HashPassword(user, "Terus@123");
      //   users.Add(user);
      // }
      // builder.Entity<AppUser>().HasData(users);
    }
  }
}