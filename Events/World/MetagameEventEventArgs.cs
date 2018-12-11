using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events.World
{
    public class MetagameEventEventArgs : System.EventArgs
    {
        public Payload Payload { get; set; }

        public class Rootobject
        {

            [JsonProperty("service")]
            public string Service { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
    }
}
