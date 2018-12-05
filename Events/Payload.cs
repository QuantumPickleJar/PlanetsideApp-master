using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    class Payload
    {

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

        public class EventPayload
        {

            
            //public EventPayload(string eventname, string timestamp, string worldid, string zoneid)
            //{
            //    Events.Payload.EventPayload Event = new Events.Payload.EventPayload(eventname, timestamp, worldid, zoneid);
            //}
            /// <summary>
            /// name of event to subscribe to 
            /// (
            /// </summary>
            /// 
            [JsonProperty("event_name")]
            public string Event_name { get; set; }

            [JsonProperty("timestamp")]
            public string Timestamp { get; set; }

            [JsonProperty("world_id")]
            public string World_id { get; set; }

            [JsonProperty("zone_id")]
            public string Zone_id { get; set; }

        }

    }
}
