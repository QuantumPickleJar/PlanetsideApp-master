using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PsApp.Gettables;

namespace PsApp
{
    public class ZoneResult
    {
        public ZoneResult() { }
        [JsonProperty("zone_list")]
        public List<ZoneList> zoneList { get; set; }
        public int returned { get; set; }

    }
}
