using Newtonsoft.Json;
using reactive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace reactive.Models.DbModels
{
    public class RecipeStep
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public RecipeStepType Type { get; set; }
        public Object Description { get; set; }
    }
}
