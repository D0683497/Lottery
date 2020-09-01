using System;
using System.Collections.Generic;
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

                #region Data

                logger.LogInformation("開始創建資料");
                InsertData(services, logger);
                logger.LogInformation("創建資料完成");

                #endregion

            }
        }

        private static void InsertData(IServiceProvider services, ILogger<SeedData> logger)
        {
            try
            {
                var dbContext = services.GetRequiredService<ApplicationDbContext>();

                var students = new List<Student>();
                for (int i = 0; i < 4000; i++)
                {
                    students.Add(new Student
                    {
                        StudentNID = Guid.NewGuid().ToString().Remove(8).ToUpper(),
                        StudentName = Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        StudentDepartment = Guid.NewGuid().ToString().Remove(10)
                    });
                }
                logger.LogInformation("建立 Students 資料");
                
                var staffs = new List<Staff>();
                for (int i = 0; i < 3000; i++)
                {
                    staffs.Add(new Staff
                    {
                        StaffNID = Guid.NewGuid().ToString().Remove(8).ToUpper(),
                        StaffName = Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        StaffDepartment = Guid.NewGuid().ToString().Remove(10)
                    });
                }
                logger.LogInformation("建立 Staffs 資料");

                var round = new Round
                {
                    RoundName = "新鮮人成長營",
                    Students = students,
                    Staffs = staffs
                };

                dbContext.Rounds.Add(round);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                logger.LogError($"發生未知錯誤\n{e.ToString()}");
                throw;
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