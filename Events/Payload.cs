using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    class Payload
    {

        public class Rootobject
        {
            /// <summary>
            /// A payload contains a payload.  Payloads are RECEIVE-ONLY
            /// 
            /// </summary>
            public Event newEvent { get; set; }
            
            /// <summary>
            ///  Property we use to bind the TextCell Text property to
            /// </summary>
            public string OutputString { get; set; }
            
            /// <summary>
            /// type of service that we are doing.  
            /// Subscribe
            /// </summary>
            /// <remarks>This property will likely be used for filtering </remarks>
            [JsonProperty("service")]
            public string Service { get; set; }
           
            public string Type { get; set; }
        }

        public class Event
        {

            public Event(string eventname, string timestamp, string worldid, string zoneid)
            {
                Events.Payload.Event payload = new Events.Payload.Event(eventname, timestamp, worldid, zoneid);
            }
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
