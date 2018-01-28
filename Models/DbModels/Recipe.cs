using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DbModels
{
    public class Recipe
    {
        [JsonProperty(PropertyName = "id")]
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual Guid UserId { get; set; }
        public virtual DateTime Date { get; set; } = DateTime.Now;
        public virtual string Name { get; set; }
        public RecipeStep[] RecipeStep { get; set; }
    }
}
