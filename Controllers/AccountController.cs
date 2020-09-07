using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Lottery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public AccountController(
            ILogger<AccountController> logger, 
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("register/admin", Name = nameof(CreateAdminUser))]
        public async Task<IActionResult> CreateAdminUser(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            using (var scope = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var createUserResult = await _userManager.CreateAsync(user, model.Password);
                    if (createUserResult.Succeeded)
                    {
                        _logger.LogInformation($"建立{user.Email}成功");
                        var currentUser = await _userManager.FindByNameAsync(model.UserName);
                        var addRoleResult = await _userManager.AddToRoleAsync(currentUser, "Admin");
                        if (addRoleResult.Succeeded)
                        {
                            _logger.LogInformation($"添加{user.Email}角色成功");
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(currentUser.Id) ? "" : currentUser.Id),
                                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(currentUser.UserName) ? "" : currentUser.UserName),
                                new Claim(ClaimTypes.Email, string.IsNullOrEmpty(currentUser.Email) ? "" : currentUser.Email),
                                new Claim(ClaimTypes.MobilePhone, string.IsNullOrEmpty(currentUser.PhoneNumber) ? "" : currentUser.PhoneNumber)
                            };
                            var addClaimResult = await _userManager.AddClaimsAsync(currentUser, claims);
                            if (addClaimResult.Succeeded)
                            {
                                _logger.LogInformation($"添加{user.Email}聲明成功");
                                await scope.CommitAsync();
                                _logger.LogInformation($"註冊{user.Email}成功");
                                return Ok();
                            }
                            _logger.LogWarning($"添加{user.Email}聲明失敗");
                        }
                        _logger.LogWarning($"添加{user.Email}角色失敗");
                    }
                    
                    _logger.LogWarning($"建立{user.Email}失敗");
                    await scope.RollbackAsync();
                    return BadRequest();
                }
                catch (Exception e)
                {
                    _logger.LogError($"註冊{user.Email}失敗");
                    _logger.LogError($"{e.ToString()}");
                    throw;
                }
            }
        }
        
        [HttpPost("register/host", Name = nameof(CreateHostUser))]
        public async Task<IActionResult> CreateHostUser(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            using (var scope = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var createUserResult = await _userManager.CreateAsync(user, model.Password);
                    if (createUserResult.Succeeded)
                    {
                        _logger.LogInformation($"建立{user.Email}成功");
                        var currentUser = await _userManager.FindByNameAsync(model.UserName);
                        var addRoleResult = await _userManager.AddToRoleAsync(currentUser, "Host");
                        if (addRoleResult.Succeeded)
                        {
                            _logger.LogInformation($"添加{user.Email}角色成功");
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(currentUser.Id) ? "" : currentUser.Id),
                                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(currentUser.UserName) ? "" : currentUser.UserName),
                                new Claim(ClaimTypes.Email, string.IsNullOrEmpty(currentUser.Email) ? "" : currentUser.Email),
                                new Claim(ClaimTypes.MobilePhone, string.IsNullOrEmpty(currentUser.PhoneNumber) ? "" : currentUser.PhoneNumber)
                            };
                            var addClaimResult = await _userManager.AddClaimsAsync(currentUser, claims);
                            if (addClaimResult.Succeeded)
                            {
                                _logger.LogInformation($"添加{user.Email}聲明成功");
                                await scope.CommitAsync();
                                _logger.LogInformation($"註冊{user.Email}成功");
                                return Ok();
                            }
                            _logger.LogWarning($"添加{user.Email}聲明失敗");
                        }
                        _logger.LogWarning($"添加{user.Email}角色失敗");
                    }
                    
                    _logger.LogWarning($"建立{user.Email}失敗");
                    await scope.RollbackAsync();
                    return BadRequest();
                }
                catch (Exception e)
                {
                    _logger.LogError($"註冊{user.Email}失敗");
                    _logger.LogError($"{e.ToString()}");
                    throw;
                }
            }
        }
        
        [HttpPost("register/client", Name = nameof(CreateClientUser))]
        public async Task<IActionResult> CreateClientUser(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            using (var scope = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var createUserResult = await _userManager.CreateAsync(user, model.Password);
                    if (createUserResult.Succeeded)
                    {
                        _logger.LogInformation($"建立{user.Email}成功");
                        var currentUser = await _userManager.FindByNameAsync(model.UserName);
                        var addRoleResult = await _userManager.AddToRoleAsync(currentUser, "Client");
                        if (addRoleResult.Succeeded)
                        {
                            _logger.LogInformation($"添加{user.Email}角色成功");
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(currentUser.Id) ? "" : currentUser.Id),
                                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(currentUser.UserName) ? "" : currentUser.UserName),
                                new Claim(ClaimTypes.Email, string.IsNullOrEmpty(currentUser.Email) ? "" : currentUser.Email),
                                new Claim(ClaimTypes.MobilePhone, string.IsNullOrEmpty(currentUser.PhoneNumber) ? "" : currentUser.PhoneNumber)
                            };
                            var addClaimResult = await _userManager.AddClaimsAsync(currentUser, claims);
                            if (addClaimResult.Succeeded)
                            {
                                _logger.LogInformation($"添加{user.Email}聲明成功");
                                await scope.CommitAsync();
                                _logger.LogInformation($"註冊{user.Email}成功");
                                return Ok();
                            }
                            _logger.LogWarning($"添加{user.Email}聲明失敗");
                        }
                        _logger.LogWarning($"添加{user.Email}角色失敗");
                    }
                    
                    _logger.LogWarning($"建立{user.Email}失敗");
                    await scope.RollbackAsync();
                    return BadRequest();
                }
                catch (Exception e)
                {
                    _logger.LogError($"註冊{user.Email}失敗");
                    _logger.LogError($"{e.ToString()}");
                    throw;
                }
            }
        }
    }
}