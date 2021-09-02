using Newtonsoft.Json;

namespace BarHelper_Android.Models
{
    public class Liq
    {
        [JsonProperty(PropertyName = "liqID")]
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
    }
}