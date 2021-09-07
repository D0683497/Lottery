using System.Threading;
using System.Threading.Tasks;
using Lottery.Data.EntityConfigurations;
using Lottery.Entities;
using Lottery.Entities.Activity;
using Lottery.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Data
{
    public class LotteryDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public LotteryDbContext(DbContextOptions<LotteryDbContext> options) : base(options)
        {
            
        }
        
        /// <summary>
        /// 使用者
        /// </summary>
        public override DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// 使用者聲明
        /// </summary>
        public override DbSet<ApplicationUserClaim> UserClaims { get; set; }

        /// <summary>
        /// 使用者登入提供者
        /// </summary>
        public override DbSet<ApplicationUserLogin> UserLogins { get; set; }

        /// <summary>
        /// 使用者驗證權杖
        /// </summary>
        public override DbSet<ApplicationUserToken> UserTokens { get; set; }
        
        /// <summary>
        /// 使用者與角色之間的連結
        /// </summary>
        public override DbSet<ApplicationUserRole> UserRoles { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public override DbSet<ApplicationRole> Roles { get; set; }

        /// <summary>
        /// 角色聲明
        /// </summary>
        public override DbSet<ApplicationRoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// 活動
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// 活動聲明
        /// </summary>
        public DbSet<EventClaim> EventClaims { get; set; }

        /// <summary>
        /// 講池
        /// </summary>
        public DbSet<Pool> Pools { get; set; }

        /// <summary>
        /// 獎品
        /// </summary>
        public DbSet<Prize> Prizes { get; set; }

        /// <summary>
        /// 參與者
        /// </summary>
        public DbSet<Participant> Participants { get; set; }

        /// <summary>
        /// 參與者聲明
        /// </summary>
        public DbSet<ParticipantClaim> ParticipantClaims { get; set; }

        /// <summary>
        /// 活動圖片
        /// </summary>
        public DbSet<EventImage> EventImages { get; set; }

        /// <summary>
        /// 獎品圖片
        /// </summary>
        public DbSet<PrizeImage> PrizeImages { get; set; }

        /// <summary>
        /// 申請表
        /// </summary>
        public DbSet<ApplyForm> ApplyForms { get; set; }

        /// <summary>
        /// 活動使用者
        /// </summary>
        public DbSet<EventUser> EventUsers { get; set; }
        
        /// <summary>
        /// 網站設定
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.EnableAutoHistory();
            
            UserConfigurations.UserRelation(builder);
            
            ActivityConfigurations.ActivityRelation(builder);

            builder.Entity<ApplyForm>();

            builder.Entity<EventUser>(b =>
            {
                b.HasKey(eu => new { eu.EventId, eu.UserId });
            });
            
            builder.Entity<Setting>();
        }
        
        public override int SaveChanges()
        {
            this.EnsureAutoHistory();
            return base.SaveChanges();
        }
    
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.EnsureAutoHistory();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}