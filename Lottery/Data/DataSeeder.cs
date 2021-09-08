using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Lottery.Entities;
using Lottery.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lottery.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<DataSeeder>>();
                var dbContext = services.GetRequiredService<LotteryDbContext>();
                
                logger.LogInformation("開始創建資料庫");
                if (await dbContext.Database.EnsureCreatedAsync())
                {
                    logger.LogInformation("開始創建角色");
                    await CreateRoleAsync(services, logger);
                    logger.LogInformation("創建角色完成");
                    
                    logger.LogInformation("開始創建使用者");
                    await CreateUserAsync(services, logger);
                    logger.LogInformation("創建角色完成");
                    
                    logger.LogInformation("開始創建網站設定");
                    await CreateSettingAsync(services, logger);
                    logger.LogInformation("創建網站設定完成");
                }
                else
                {
                    logger.LogInformation("資料庫已存在");
                }
                
                logger.LogInformation("開始創建資料夾");
                CreateFolder(logger);
                logger.LogInformation("創建資料夾完成");
            }
        }
        
        private static async Task CreateRoleAsync(IServiceProvider services, ILogger<DataSeeder> logger)
        {
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
            
            var administrator = new ApplicationRole
            {
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpperInvariant()
            };
            await roleManager.CreateAsync(administrator);
            await roleManager.AddClaimAsync(administrator, new Claim(ClaimTypes.Role, "Administrator"));
        }
        
        private static async Task CreateUserAsync(IServiceProvider services, ILogger<DataSeeder> logger)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser
            {
                Name = "管理員",
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                IsEnable = true
            };
            await userManager.CreateAsync(user, "Admin1234");
            await userManager.AddToRoleAsync(user, "Administrator");
        }
        
        private static async Task CreateSettingAsync(IServiceProvider services, ILogger<DataSeeder> logger)
        {
            var dbContext = services.GetRequiredService<LotteryDbContext>();
            await dbContext.Settings.AddRangeAsync(new List<Setting>
            {
                new Setting { Name = "event", Value = "https://sscurl.fcu.edu.tw/" },
                new Setting { Name = "privacy", Value = "https://www.fcu.edu.tw/privacy/" },
                new Setting { Name = "facebook", Value = "https://www.facebook.com/fcussc/" },
                new Setting { Name = "instagram", Value = "https://www.instagram.com/fcu.cdc/" },
                new Setting { Name = "website", Value = "https://ssc.fcu.edu.tw/" },
                new Setting { Name = "github", Value = "https://github.com/fcu-ssc/" },
                new Setting { Name = "credits", Value = "https://github.com/D0683497/Lottery" }
            });
            await dbContext.SaveChangesAsync();
        }
        
        private static void CreateFolder(ILogger<DataSeeder> logger)
        {
            var paths = new List<string> { "event", "pool", "prize" };
            
            foreach (var path in paths)
            {
                if (!Directory.Exists($"wwwroot{Path.DirectorySeparatorChar}{path}"))
                {
                    Directory.CreateDirectory($"wwwroot{Path.DirectorySeparatorChar}{path}");
                    logger.LogInformation($"{path} 建立成功");
                }
                else
                {
                    logger.LogInformation($"{path} 已經存在");
                }
            }
        }
    }
}