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

                #region Student

                var students = new List<Attendee>();
                for (int i = 0; i < 4000; i++)
                {
                    students.Add(new Attendee
                    {
                        AttendeeNID = "學NID" + Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        AttendeeName = "學" + Guid.NewGuid().ToString().Remove(3).ToUpper(),
                        AttendeeDepartment = "學D" + Guid.NewGuid().ToString().Remove(8)
                    });
                }
                var studentItem = new Item
                {
                    ItemName = "學生",
                    Attendees = students
                };
                dbContext.Items.Add(studentItem);
                dbContext.SaveChanges();
                logger.LogInformation("建立 Students 資料");

                #endregion

                #region Staff

                var staffs = new List<Attendee>();
                for (int i = 0; i < 3000; i++)
                {
                    staffs.Add(new Attendee
                    {
                        AttendeeNID = "工NID" + Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        AttendeeName = Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        AttendeeDepartment = "工D" + Guid.NewGuid().ToString().Remove(8)
                    });
                }
                var staffItem = new Item
                {
                    ItemName = "工作人員",
                    Attendees = staffs
                };
                dbContext.Items.Add(staffItem);
                dbContext.SaveChanges();
                logger.LogInformation("建立 Staffs 資料");

                #endregion

                #region Other

                var others = new List<Attendee>();
                for (int i = 0; i < 2000; i++)
                {
                    others.Add(new Attendee
                    {
                        AttendeeNID = "其NID" + Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        AttendeeName = Guid.NewGuid().ToString().Remove(4).ToUpper(),
                        AttendeeDepartment = "其D" + Guid.NewGuid().ToString().Remove(8)
                    });
                }
                var otherItem = new Item
                {
                    ItemName = "其他",
                    Attendees = others
                };
                dbContext.Items.Add(otherItem);
                dbContext.SaveChanges();
                logger.LogInformation("建立 Others 資料");

                #endregion
                
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