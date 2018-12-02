using Newtonsoft.Json;
namespace PsApp
{
    public class FacilityControlChangedEventArgs : System.EventArgs
    {
        // define the properties which describe the circumstances of the event

        
        public class Rootobject
        {
            public FacilityChangePayload facilityChangePayload { get; set; }
            public string service { get; set; }
            public string type { get; set; }
        }

        public class FacilityChangePayload
        {
            
            [JsonProperty("duration_held")]
            public string duration_held { get; set; }

            [JsonProperty("facility_id")]
            public string facility_id { get; set; }
            
            [JsonProperty("new_faction_id")]
            public string new_faction_id { get; set; }
            
            [JsonProperty("old_faction_id")]
            public string old_faction_id { get; set; }
            
            [JsonProperty("outfit_id")]
            public string outfit_id { get; set; }
            
            
        }
    }
}