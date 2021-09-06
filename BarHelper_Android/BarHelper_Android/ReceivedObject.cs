using Newtonsoft.Json;

namespace BarHelper_Android
{
    public class ReceivedObject
    {
        [JsonProperty(propertyName:"mess")]
        public int Mess { get; set; }
    }
}