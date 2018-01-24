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
        SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) {
            _userStore = userStore;
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
        public IActionResult Signin([FromBody]UserSigninDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, false);
                Task<ApplicationUser> au = _userManager.FindByNameAsync(dto.UserName);
               
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("1234567890123456");// (_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, au.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    Token = tokenString
                });

                /*Task<Microsoft.AspNetCore.Identity.SignInResult> result = 
                    _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, false);

                if (result.IsFaulted)
                    return BadRequest(result.Exception.Message);
                else
                    return Ok(result);*/
            }
            catch (Exception e)
            {
                return BadRequest(e?.Message);
            }
        }


    }
}