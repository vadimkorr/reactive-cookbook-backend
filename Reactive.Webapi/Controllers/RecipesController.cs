using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using reactive.Models.DbModels;
using reactive.Models.DTO;
using Reactive.DAL.CosmosDb;
using Reactive.DAL.Interfaces;

namespace Reactive.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Recipes")]
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

        [AllowAnonymous]
        [HttpPost]
        [Route("submit")]
        public IActionResult Submit([FromBody]SubmitRecipeDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ApplicationUser user = _userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
                Recipe recipe = new Recipe() {
                    UserId = user.Id,
                    Name = dto.Name,
                    RecipeStep = dto.RecipeSteps
                };
                var result = _recipeQueries.Submit(recipe).GetAwaiter().GetResult();
                return Ok(result);
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