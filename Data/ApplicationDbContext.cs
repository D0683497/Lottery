using Lottery.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Round> Rounds { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>()
                .HasOne(student => student.Round)
                .WithMany(round => round.Students)
                .HasForeignKey(student => student.RoundId);
            
            builder.Entity<Staff>()
                .HasOne(staff => staff.Round)
                .WithMany(round => round.Staffs)
                .HasForeignKey(staff => staff.RoundId);
            
            builder.Entity<Winner>()
                .HasOne(winner => winner.Round)
                .WithMany(round => round.Winners)
                .HasForeignKey(winner => winner.RoundId);

            builder.Entity<Student>()
                .HasOne(student => student.Winner)
                .WithMany(winner => winner.Students)
                .HasForeignKey(student => student.WinnerId);
            
            builder.Entity<Staff>()
                .HasOne(staff => staff.Winner)
                .WithMany(winner => winner.Staffs)
                .HasForeignKey(staff => staff.WinnerId);
        }
    }
}