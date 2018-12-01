using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    class Event
    {


        public class Rootobject
        {
            /// <summary>
            /// Every message contains a payload.  Payloads are RECEIVE-ONLY
            /// 
            /// </summary>
            public Payload payload { get; set; }
            
            /// <summary>
            /// type of service that we are doing
            /// Subscribe
            /// </summary>
            [JsonProperty("service")]
            public string Service { get; set; }
           
            public string Type { get; set; }
        }

        public class Payload
        {
            public int Achievement_id { get; set; }
            public string Character_id { get; set; }
            /// <summary>
            /// name of event to subscribe to 
            /// (
            /// </summary>
            public string Event_name { get; set; }
            public string Timestamp { get; set; }   
            public string World_id { get; set; }
            public string Zone_id { get; set; }
        }

    }
}
