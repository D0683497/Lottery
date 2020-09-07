﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using Lottery.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
                
                #region User

                logger.LogInformation("開始創建使用者");
                CreateUser(services, logger);
                logger.LogInformation("創建使用者完成");

                #endregion

                #region Data

                // logger.LogInformation("開始創建資料");
                // InsertData(services, logger);
                // logger.LogInformation("創建資料完成");

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

                #region Admin

                var result = roleManager.CreateAsync(new ApplicationRole { Name = "Admin" }).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立Admin角色成功");

                    var admin = roleManager.FindByNameAsync("Admin").Result;
                    var adminClaim = new Claim(ClaimTypes.Role, "Admin");

                    result = roleManager.AddClaimAsync(admin, adminClaim).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation("建立Admin角色聲明成功");
                    }
                    else
                    {
                        logger.LogError("建立Admin角色聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("建立Admin角色失敗");
                }

                #endregion

                #region Host

                result = roleManager.CreateAsync(new ApplicationRole { Name = "Host" }).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立Host角色成功");

                    var host = roleManager.FindByNameAsync("Host").Result;
                    var hostClaim = new Claim(ClaimTypes.Role, "Host");

                    result = roleManager.AddClaimAsync(host, hostClaim).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation("建立Host角色聲明成功");
                    }
                    else
                    {
                        logger.LogError("建立Host角色聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("建立Host角色失敗");
                }

                #endregion

                #region Client

                result = roleManager.CreateAsync(new ApplicationRole { Name = "Client" }).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立Client角色成功");

                    var client = roleManager.FindByNameAsync("Client").Result;
                    var clientClaim = new Claim(ClaimTypes.Role, "Client");

                    result = roleManager.AddClaimAsync(client, clientClaim).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation("建立Client角色聲明成功");
                    }
                    else
                    {
                        logger.LogError("建立Client角色聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("建立Client角色失敗");
                }

                #endregion

            }
            catch (Exception e)
            {
                logger.LogError($"發生未知錯誤\n{e.ToString()}");
                throw;
            }
        }

        private static void CreateUser(IServiceProvider services, ILogger<SeedData> logger)
        {
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                #region Admin
                
                var admin = new ApplicationUser
                {
                    Email = configuration["UserSettings:Email"],
                    EmailConfirmed = true,
                    UserName = configuration["UserSettings:UserName"]
                };

                var result = userManager.CreateAsync(admin, configuration["UserSettings:Password"]).Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("建立Admin使用者成功");
                }
                else
                {
                    logger.LogError("建立Admin使用者失敗");
                }

                var currentUser = userManager.FindByNameAsync(configuration["UserSettings:UserName"]).Result;
                result = userManager.AddToRoleAsync(currentUser, "Admin").Result;
                if (result.Succeeded)
                {
                    logger.LogInformation("Admin使用者添加角色成功");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(currentUser.Id) ? "" : currentUser.Id),
                        new Claim(ClaimTypes.Name, string.IsNullOrEmpty(currentUser.UserName) ? "" : currentUser.UserName),
                        new Claim(ClaimTypes.Email, string.IsNullOrEmpty(currentUser.Email) ? "" : currentUser.Email),
                        new Claim(ClaimTypes.MobilePhone, string.IsNullOrEmpty(currentUser.PhoneNumber) ? "" : currentUser.PhoneNumber)
                    };
                    var addClaimResult = userManager.AddClaimsAsync(currentUser, claims).Result;
                    if (addClaimResult.Succeeded)
                    {
                        logger.LogInformation("Admin使用者添加聲明成功");
                    }
                    else
                    {
                        logger.LogError("Admin使用者添加聲明失敗");
                    }
                }
                else
                {
                    logger.LogError("Admin使用者添加角色失敗");
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