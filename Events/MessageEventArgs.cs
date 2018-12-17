using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events
{
    public class MessageEventArgs : System.EventArgs
    {
        [JsonProperty("connected")]
        public string connectionStatus { get; set; }
    }
}
