using Lottery.Entities;
using Lottery.Entities.Activity;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Data.EntityConfigurations
{
    public static class ActivityConfigurations
    {
        public static void ActivityRelation(ModelBuilder builder)
        {
            builder.Entity<Event>(b =>
            {
                b.HasMany(e => e.Pools)
                    .WithOne(p => p.Event)
                    .HasForeignKey(p => p.EventId);
                b.HasMany(e => e.Fields)
                    .WithOne(c => c.Event)
                    .HasForeignKey(c => c.EventId);
                b.HasOne(e => e.ApplyForm)
                    .WithOne(a => a.Event)
                    .HasForeignKey<ApplyForm>(a => a.EventId);
                b.HasOne(e => e.Image)
                    .WithOne(i => i.Event)
                    .HasForeignKey<EventImage>(i => i.EventId);
                b.HasMany(e => e.Users)
                    .WithOne(eu => eu.Event)
                    .HasForeignKey(eu => eu.EventId);
            });

            builder.Entity<Field>(b =>
            {
                b.HasMany(e => e.ParticipantClaims)
                    .WithOne(pc => pc.Field)
                    .HasForeignKey(pc => pc.EventClaimId);
            });

            builder.Entity<Pool>(b =>
            {
                b.HasMany(e => e.Prizes)
                    .WithOne(prize => prize.Pool)
                    .HasForeignKey(prize => prize.PoolId);
                b.HasMany(e => e.Participants)
                    .WithOne(participant => participant.Pool)
                    .HasForeignKey(participant => participant.PoolId);
            });

            builder.Entity<Prize>(b =>
            {
                b.HasOne(e => e.Image)
                    .WithOne(i => i.Prize)
                    .HasForeignKey<PrizeImage>(i => i.PrizeId);
                b.HasMany(e => e.ParticipantPrizes)
                    .WithOne(pp => pp.Prize)
                    .HasForeignKey(pp => pp.PrizeId);
            });

            builder.Entity<Participant>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne(c => c.Participant)
                    .HasForeignKey(c => c.ParticipantId);
                b.HasMany(e => e.ParticipantPrizes)
                    .WithOne(pp => pp.Participant)
                    .HasForeignKey(pp => pp.ParticipantId);
            });

            builder.Entity<ParticipantClaim>();

            builder.Entity<ParticipantPrize>();

            builder.Entity<EventImage>();

            builder.Entity<PrizeImage>();
        }
    }
}