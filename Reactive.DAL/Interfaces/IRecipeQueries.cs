using Reactive.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.DAL.Interfaces
{
    public interface IRecipeQueries
    {
        Task<bool> Submit(Recipe recipe);
        Task<IEnumerable<Recipe>> GetRecipesByUserId(Guid userId);
    }
}
