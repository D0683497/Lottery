using System;
using System.Security.Claims;
using Lottery.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lottery.Data
{
    public class SeedData
    {
        public static void EnsureSeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<SeedData>>();

                #region Role

                logger.LogInformation("開始創建角色及角色聲明");
                CreateRole(services, logger);
                logger.LogInformation("創建角色及角色聲明完成");

                #endregion

            }
        }

        private static void CreateRole(IServiceProvider services, ILogger<SeedData> logger)
        {
            try
            {
                var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                #region GeneralUser

                var result = roleManager.CreateAsync(new ApplicationRole { Name = "GeneralUser" }).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立GeneralUser角色成功");

                    var generalUser = roleManager.FindByNameAsync("GeneralUser").Result;
                    var generalUserClaim = new Claim(ClaimTypes.Role, "GeneralUser");

                    result = roleManager.AddClaimAsync(generalUser, generalUserClaim).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation("建立GeneralUser角色聲明成功");
                    }
                    else
                    {
                        logger.LogError("建立GeneralUser角色聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("建立GeneralUser角色失敗");
                }

                #endregion

                #region AdminUser

                result = roleManager.CreateAsync(new ApplicationRole { Name = "AdminUser" }).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立AdminUser角色成功");

                    var adminUser = roleManager.FindByNameAsync("AdminUser").Result;
                    var adminUserClaim = new Claim(ClaimTypes.Role, "AdminUser");

                    result = roleManager.AddClaimAsync(adminUser, adminUserClaim).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation("建立AdminUser角色聲明成功");
                    }
                    else
                    {
                        logger.LogError("建立AdminUser角色聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("建立AdminUser角色失敗");
                }

                #endregion

            }
            catch (Exception e)
            {
                logger.LogError($"發生未知錯誤\n{e.ToString()}");
                throw;
            }
        }
    }
}