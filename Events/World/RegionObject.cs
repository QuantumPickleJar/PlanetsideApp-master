using Newtonsoft.Json;
namespace PsApp.Events.World
{
    public class RegionObject
    {

        [JsonProperty("region_id")]
        public string region_id { get; set; }

        [JsonProperty("zone_id")]
        public string zone_id { get; set; }

        [JsonProperty("name")]
        public Name name { get; set; }
        
        public class Name
        {
            public string en { get; set; }
        }
        
    }
}