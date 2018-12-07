using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PsApp.Gettables
{
    public class Continent
    { 
        [JsonProperty("zone_id")]
        public string Continent_Id{ get; set; }

        [JsonProperty("name")]
        public string continentName { get; set; }
        
        [JsonProperty("regions")]
        public List<Events.World.RegionObject> regions { get; set; }
    }
}
