using reactive.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DTO
{
    public class SubmitRecipeDto
    {
        public virtual DateTime Date { get; set; } = DateTime.Now;
        public virtual string Name { get; set; }
        public RecipeStep[] RecipeSteps { get; set; }
    }
}
