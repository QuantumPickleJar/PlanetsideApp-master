using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

//world-level event payload 
namespace PsApp.Events
{
    class ContinentUnlockEvent : Payload
    {
        public class RootObject
        {
            [JsonProperty("triggering_faction")]
            public string Triggering_faction { get; set; }

            [JsonProperty("previous_faction")]
            public string Previous_faction { get; set; }

            [JsonProperty("vs_population")]
            public string Vs_population { get; set; }

            [JsonProperty("nc_population")]
            public string Nc_population { get; set; }

            [JsonProperty("tr_population")]
            public string Tr_population { get; set; }

            [JsonProperty("metagame_event_id")]
            public string Metagame_event_id { get; set; }

            [JsonProperty("event_type")]
            public string Event_type { get; set; }

        }

    }
}
