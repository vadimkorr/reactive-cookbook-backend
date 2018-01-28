using reactive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DbModels
{
    public class RecipeStepWait : IRecipeStepDescription
    {
        public int TimeAmount { get; set; }
        public TimeUnitEnum TimeUnits { get; set; }
    }
}
