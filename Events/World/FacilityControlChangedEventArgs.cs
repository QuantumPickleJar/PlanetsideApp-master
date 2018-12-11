using Newtonsoft.Json;
using PsApp.Events;

namespace PsApp
{
    public class FacilityControlChangedEventArgs : System.EventArgs
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