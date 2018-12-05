using Newtonsoft.Json;
namespace PsApp
{
    public class FacilityControlChangedEventArgs : System.EventArgs
    {
        // define the properties which describe the circumstances of the event
        [JsonProperty("duration_held")]
        public string Duration_held { get; set; }

        [JsonProperty("facility_id")]
        public string Facility_id { get; set; }

        [JsonProperty("new_faction_id")]
        public string New_faction_id { get; set; }

        [JsonProperty("old_faction_id")]
        public string Old_faction_id { get; set; }

        [JsonProperty("outfit_id")]
        public string Outfit_id { get; set; }


        public class Rootobject
        {
            public FacilityChangePayload facilityChangePayload { get; set; }
            [JsonProperty("service")]
            public string Service { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
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