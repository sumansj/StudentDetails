using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentDetails.Models;

namespace StudentDetails.Data
{
    public class StudentDbContext : IdentityDbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> dboptions) : base(dboptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                  .Ignore(c => c.ConcurrencyStamp)
                                  .Ignore(c => c.EmailConfirmed)
                                  .Ignore(c => c.LockoutEnabled)
                                  .Ignore(c => c.LockoutEnd)
                                  .Ignore(c => c.NormalizedEmail)
                                  .Ignore(c => c.PhoneNumber)
                                  .Ignore(c => c.PhoneNumberConfirmed)
                                  .Ignore(c => c.SecurityStamp)
                                  .Ignore(c => c.TwoFactorEnabled);
            builder.Entity<IdentityRole>().Ignore(d => d.ConcurrencyStamp);
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
        }
        public DbSet<Students> Students { set; get; }
    }
}
