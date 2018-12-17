using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
//world-level event payload
namespace PsApp.Events
{
    public class FacilityControlChangedEvent : Payload
    {
        public class RootObject
        {
            [JsonProperty("old_faction_id")]
            public int Old_faction_id { get; set; }

            [JsonProperty("outfit_id")]
            public string Outfit_id { get; set; }

            [JsonProperty("new_faction_id")]
            public int New_faction_id { get; set; }

            [JsonProperty("facility_id")]
            public string Facility_id { get; set; }

            //will be in an Epoch format
            [JsonProperty("duration_held")]
            public string Duration_held { get; set; }
            
        }

    }
}
