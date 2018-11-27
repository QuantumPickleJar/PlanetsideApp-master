using Newtonsoft.Json;

namespace PlanetsideApi
{
    public class Name
    {
        [JsonProperty("first")]
        public string First { get; set; }
    }
}