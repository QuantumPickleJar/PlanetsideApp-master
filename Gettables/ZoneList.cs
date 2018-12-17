using PsApp.Events.World;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{
    public class ZoneList
    {
        [JsonProperty("zone_id")]
        public string zone_id { get; set; }
        //public string code { get; set; }
        //public string hex_size { get; set; }
        //public Name name { get; set; }

        [JsonProperty("regions")]
        public List<RegionObject> regions { get; set; }
        
    }
}
