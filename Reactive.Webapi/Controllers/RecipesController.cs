using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Newtonsoft.Json.Linq;
using reactive.Models.DbModels;
using reactive.Models.DTO;
using reactive.Models.Enums;
using Reactive.DAL.CosmosDb;
using Reactive.DAL.Interfaces;

//using Microsoft.AspNet.Identity;

namespace Reactive.Webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/Recipes")]
    public class RecipesController : Controller
    {
        //private readonly ClaimsPrincipal _caller;
        UserManager<ApplicationUser> _userManager;
        IRecipeQueries _recipeQueries;

        public RecipesController(
            UserManager<ApplicationUser> userManager,
            IRecipeQueries recipeQueries)//,
            //ClaimsPrincipal caller)
        {
            //_caller = caller;
            _userManager = userManager;
            _recipeQueries = recipeQueries;
        }

        [Authorize]
        //[AllowAnonymous]
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit([FromBody]SubmitRecipeDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var n = User.Identity.Name;
                //RequestContext.Principal
                //var email = User.FindFirst("sub")?.Value;
                //var userId = HttpContext.User.GetUserId();
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                //var username = Request.Form["username"];
                //var u = _caller.Claims.Select(c => new { c.Type, c.Value });
                //ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);


                Recipe recipe = new Recipe()
                {
                    UserId = Guid.NewGuid(),
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