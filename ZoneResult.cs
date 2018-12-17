using Newtonsoft.Json;
using System.Collections.Generic;

namespace PsApp
{
    public class ZoneResult
    {
        [JsonProperty("zone_list")]
        public List<Gettables.ZoneList> zoneList { get; set; }
        public int returned { get; set; }
    }
}