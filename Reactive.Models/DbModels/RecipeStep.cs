using Newtonsoft.Json;
using Reactive.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reactive.Models.DbModels
{
    public class RecipeStep
    {
        //[JsonProperty(PropertyName = "id")]
        //public Guid Id { get; set; } = Guid.NewGuid();
        public RecipeStepType Type { get; set; }
        public Object Description { get; set; }
    }
}
