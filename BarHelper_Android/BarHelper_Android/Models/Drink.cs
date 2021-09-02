using System.Collections.Generic;
using Newtonsoft.Json;

namespace BarHelper_Android.Models
{
    public class Drink
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "recipe")]
        public List<Component> Recipe { get; set; }
    }
}