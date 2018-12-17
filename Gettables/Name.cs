using Newtonsoft.Json;

namespace PsApp
{
    public class Name
    {
        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("en")]
        public string En { get; set; }
    }
}