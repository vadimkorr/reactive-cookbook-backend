using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reactive.Models.DTO;
using Reactive.Models.DbModels;

namespace Reactive.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin([FromBody]UserSigninDto dto)
        {
            if (dto == null)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                throw new Exception("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Password is incorrect");

            var token = new AccessToken(dto.UserName, user.Id.ToString());

            return Json(new
            {
                Token = token.Token
            });
        }


    }
}