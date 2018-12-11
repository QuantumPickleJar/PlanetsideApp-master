using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    public class ConnectionStateChangeEventArgs : System.EventArgs
    {
        public Message theMessage { get; internal set; }


        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("connected")]
        public string ConnectionStatus { get; set; }

    }
}
