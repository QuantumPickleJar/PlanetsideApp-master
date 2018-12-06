using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

//world-level event payload 
namespace PsApp.Events
{
    public class MetagameEventEvent : Payload
    {
        public class RootObject
        {
            [JsonProperty("experience_bonus")]
            public string Experience_bonus { get; set; }

            [JsonProperty("faction_nc")]
            public string Faction_nc { get; set; }

            [JsonProperty("faction_tr")]
            public string Faction_tr { get; set; }

            [JsonProperty("faction_vs")]
            public string Faction_vs { get; set; }

            [JsonProperty("metagame_event_id")]
            public string Metagame_event_id { get; set; }

            [JsonProperty("metagame_event_state")]
            public string Metagame_event_state { get; set; }
        }

    }
}
