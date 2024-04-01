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
          .WithMany(p => p.Members)
          .UsingEntity<ProjectMember>(
            l => l.HasOne(x => x.Project).WithMany(x => x.ProjectMembers).HasForeignKey(u => u.ProjectId),
            r => r.HasOne(x => x.Member).WithMany(x => x.ProjectMembers).HasForeignKey(e => e.MemberId)
          );
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
      var AdminRoleId = Guid.NewGuid().ToString();
      builder.Entity<IdentityRole>().HasData(
        new()
        {
          Id = AdminRoleId,
          Name = "Admin",
          NormalizedName = "ADMIN",
          ConcurrencyStamp = Guid.NewGuid().ToString()
        },
        new()
        {
          Name = "Manager",
          NormalizedName = "MANAGER",
          ConcurrencyStamp = Guid.NewGuid().ToString()
        },
        new()
        {
          Name = "Employee",
          NormalizedName = "EMPLOYEE",
          ConcurrencyStamp = Guid.NewGuid().ToString()
        },
        new()
        {
          Name = "Client",
          NormalizedName = "CLIENT",
          ConcurrencyStamp = Guid.NewGuid().ToString()
        }
      );

      var pass = new PasswordHasher<AppUser>();
      var admin1 = new AppUser
      {
        Id = Guid.NewGuid().ToString(),
        UserName = "terus",
        Email = "phucthinhterus@gmail.com",
        FirstName = "Phúc Thịnh",
        LastName = "Phan",
        EmailConfirmed = true,
        NormalizedEmail = "PHUCTHINHTERUS@GMAIL.COM",
        NormalizedUserName = "TERUS",
      };
      admin1.PasswordHash = pass.HashPassword(admin1, "Terus@123");
      var admin2 = new AppUser
      {
        Id = Guid.NewGuid().ToString(),
        UserName = "dev",
        Email = "anhcrafter@gmail.com",
        FirstName = "Quang Anh",
        LastName = "Đặng",
        EmailConfirmed = true,
        NormalizedEmail = "ANHCRAFTER@GMAIL.COM",
        NormalizedUserName = "DEV",
      };
      admin2.PasswordHash = pass.HashPassword(admin2, "Terus@123");
      builder.Entity<AppUser>().HasData(admin1);
      builder.Entity<AppUser>().HasData(admin2);

      builder.Entity<IdentityUserRole<string>>().HasData(
        new IdentityUserRole<string>
        {
          RoleId = AdminRoleId,
          UserId = admin1.Id
        },
        new IdentityUserRole<string>
        {
          RoleId = AdminRoleId,
          UserId = admin2.Id
        }
      );
    }
  }
}