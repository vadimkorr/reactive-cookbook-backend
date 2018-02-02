using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.Models.DbModels
{
    public class RecipeStepMake : IRecipeStepDescription
    {
        public string Process { get; set; }
    }
}
