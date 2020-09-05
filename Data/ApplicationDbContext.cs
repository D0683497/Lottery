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

        public DbSet<Item> Items { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            /* Item 跟 Attendee 一對多 */
            builder.Entity<Attendee>()
                .HasOne(attendee => attendee.Item)
                .WithMany(item => item.Attendees)
                .HasForeignKey(attendee => attendee.ItemId);

            /* Item 跟 Winner 一對多 */
            builder.Entity<Winner>()
                .HasOne(winner => winner.Item)
                .WithMany(item => item.Winners)
                .HasForeignKey(winner => winner.ItemId);
            
            /* Attendee 跟 Winner 一對多 */
            builder.Entity<Attendee>()
                .HasOne(attendee => attendee.Winner)
                .WithOne(winner => winner.Attendee)
                .HasForeignKey<Winner>(winner => winner.AttendeeId);
        }
    }
}