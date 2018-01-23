using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using reactive.Models.DTO;

namespace Reactive.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        IUserStore<ApplicationUser> _userStore;
        UserManager<ApplicationUser> _userManager;
        public AccountsController(
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager) {
            _userStore = userStore;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserRegisterDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ApplicationUser au = new ApplicationUser()
                {
                    UserName = dto.UserName,
                    Email = dto.Email
                };

                var result = _userManager.CreateAsync(au, dto.Password).GetAwaiter().GetResult();
                if (result.Errors.ToArray().Length > 0)
                    return BadRequest(string.Join(';', result.Errors.Select(e => e.Description)));
                else
                    return Ok(result);
            }
            catch (Exception e) {
                return BadRequest(e?.Message);
            }
        }
    }
}