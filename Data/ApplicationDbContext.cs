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
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Prize>()
                .HasOne(prize => prize.Round)
                .WithMany(round => round.Prizes)
                .HasForeignKey(prize => prize.RoundId);
            
            builder.Entity<Attendee>()
                .HasOne(attendee => attendee.Round)
                .WithMany(round => round.Attendees)
                .HasForeignKey(attendee => attendee.RoundId);
            
            builder.Entity<Winner>()
                .HasOne(winner => winner.Round)
                .WithMany(round => round.Winners)
                .HasForeignKey(winner => winner.RoundId);

            builder.Entity<Attendee>()
                .HasOne(attendee => attendee.Winner)
                .WithMany(winner => winner.Attendees)
                .HasForeignKey(attendee => attendee.WinnerId);

            builder.Entity<Winner>()
                .HasOne(winner => winner.Prize)
                .WithOne(prize => prize.Winner)
                .HasForeignKey<Prize>(prize => prize.WinnerId);
        }
    }
}