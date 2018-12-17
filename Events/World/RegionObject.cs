using Newtonsoft.Json;
namespace PsApp.Events.World
{
    public class RegionObject 
    {

        //[JsonProperty("region_id")]
        //public string region_id { get; set; }

        [JsonProperty("zone_id")]
        public string zone_id { get; set; }

        public string facility_id { get; set; }

        public string facility_name { get; set; }

        public string facility_type_id { get; set; }

        public string facility_type { get; set; }
        public string location_x { get; set; }
        public string location_y { get; set; }
        public string location_z { get; set; }      
        public string reward_amount { get; set; }
        public string reward_currency_id { get; set; }
        

        [JsonProperty("name")]
        public Name name { get; set; }
        
        public class Name
        {
            public string en { get; set; }
        }
        
    }
}