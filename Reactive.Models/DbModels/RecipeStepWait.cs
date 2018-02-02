using Reactive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.Models.DbModels
{
    public class RecipeStepWait : IRecipeStepDescription
    {
        public int TimeAmount { get; set; }
        public TimeUnitEnum TimeUnits { get; set; }
    }
}
