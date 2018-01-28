using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DbModels
{
    public class RecipeStepMake : IRecipeStepDescription
    {
        public string Process { get; set; }
    }
}
