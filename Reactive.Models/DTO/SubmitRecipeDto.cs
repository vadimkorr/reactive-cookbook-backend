using Reactive.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.Models.DTO
{
    public class SubmitRecipeDto
    {
        public virtual DateTime Date { get; set; } = DateTime.Now;
        public virtual string Name { get; set; }
        public RecipeStep[] RecipeSteps { get; set; }
    }
}
