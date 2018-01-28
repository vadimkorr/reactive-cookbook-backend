using reactive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DbModels
{
    public class RecipeStepPut : IRecipeStepDescription
    {
        public string Ingredient { get; set; }
        public int Count { get; set; }
        public QuantityUnitEnum QuantityUnit { get; set; }
    }
}
