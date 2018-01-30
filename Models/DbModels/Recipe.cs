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
        public virtual DateTime Date { get; set; }
        public virtual String Name { get; set; }
        public virtual RecipeStep[] RecipeSteps { get; set; }
        public override String ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
