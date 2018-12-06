using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{
    public class Message
    {
        //
        [JsonProperty("service")]
        public string Service { get; set; }

        //probably going to only use for filtering
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("action")]
        public string Action{ get; set; }
    }
}
