using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    public class Payload
    {
        //no idea why i did this
     
            //make constructor that uses if statements that check the service param to find out what to do next
        

        //make this        v     and other payload types their own classes later 
        [JsonProperty("event_name")]
        public string Event_name { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("world_id")]
        public int World_id { get; set; }

        [JsonProperty("zone_id")]
        public string Zone_id { get; set; }
        
        [JsonProperty("duration_held")]
        public long duration_held { get; set; }

        [JsonProperty("facility_id")]
        public string facility_id { get; set; }

        [JsonProperty("new_faction_id")]
        public int new_faction_id { get; set; }

        [JsonProperty("old_faction_id")]
        public int old_faction_id { get; set; }

        [JsonProperty("outfit_id")]
        public string outfit_id { get; set; }

        
        // below properties are specificlly for metagame events
        public string faction_nc { get; set; }
        public string faction_tr { get; set; }
        public string faction_vs { get; set; }
        public string instance_id { get; set; }
        public string metagame_event_id { get; set; }
        public string metagame_event_state { get; set; }
        public string metagame_event_state_name { get; set; }



        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }


        //same as above, but has a constructor
        public class EventPayload : Payload
        {


            public EventPayload(string eventname, string timestamp, string worldid, string zoneid)
            {
                Events.Payload.EventPayload Event = new Events.Payload.EventPayload(eventname, timestamp, worldid, zoneid);
            }
            /// <summary>
            /// name of event to subscribe to 
            /// (
            /// </summary>
            
        }


    }
}
