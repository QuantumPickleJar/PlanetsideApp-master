using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace PsApp
{
    public class ZoneResultList
    {
        public ZoneResultList(List<Gettables.ZoneList> zl, int r)
        {
            zoneList = zl;
            returned = r;
        }
        [JsonProperty("zone_list")]
        public List<PsApp.Gettables.ZoneList> zoneList { get; set; }
        public int returned { get; set; }
    }
}
