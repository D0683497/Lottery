using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Lottery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            ILogger<AccountController> logger, 
            ApplicationDbContext applicationDbContext, 
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
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
                        var currentUser = await _userManager.FindByNameAsync(model.UserName);
                        var addRoleResult = await _userManager.AddToRoleAsync(currentUser, "GeneralUser");
                        if (addRoleResult.Succeeded)
                        {
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
                                await scope.CommitAsync();
                                _logger.LogInformation($"註冊{currentUser.Id}成功");
                                return Ok();
                            }
                        }
                    }

                    await scope.RollbackAsync();
                    return BadRequest();
                }
                catch (Exception e)
                {
                    await scope.RollbackAsync();
                    _logger.LogError($"註冊用戶失敗\n{e.ToString()}");
                    return BadRequest();
                }
            }
        }
    }
}