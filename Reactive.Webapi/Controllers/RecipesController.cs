using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reactive.Models.DbModels;
using Newtonsoft.Json.Linq;
using Reactive.Models.DbModels;
using Reactive.Models.DTO;
using Reactive.Models.Enums;
using Reactive.DAL.Interfaces;

//using Microsoft.AspNet.Identity;

namespace Reactive.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/recipes")]
    public class RecipesController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IRecipeQueries _recipeQueries;

        public RecipesController(
            UserManager<ApplicationUser> userManager,
            IRecipeQueries recipeQueries)
        {
            _userManager = userManager;
            _recipeQueries = recipeQueries;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit([FromBody]SubmitRecipeDto dto)
        {
            if (dto == null)
                return BadRequest(ModelState);

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                Recipe recipe = new Recipe()
                {
                    UserId = Guid.Parse(identity.FindFirst("id").Value),
                    Date = dto.Date,
                    Name = dto.Name,
                    RecipeSteps = dto.RecipeSteps
                };
                var result = await _recipeQueries.Submit(recipe);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e?.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetMyRecipes()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userId = Guid.Parse(identity.FindFirst("id").Value);

                var result = await _recipeQueries.GetRecipesByUserId(userId);
                return Json(result);
            }
            catch (Exception e)
            {
                return BadRequest(e?.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Submit1([FromBody]Recipe dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e?.Message);
            }
        }
    }
}