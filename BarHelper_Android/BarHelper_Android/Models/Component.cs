using Newtonsoft.Json;

namespace BarHelper_Android.Models
{
    public class Component
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}