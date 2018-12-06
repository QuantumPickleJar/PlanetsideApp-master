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
        public string Timestamp { get; set; }

        [JsonProperty("world_id")]
        public string World_id { get; set; }

        [JsonProperty("zone_id")]
        public string Zone_id { get; set; }

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
