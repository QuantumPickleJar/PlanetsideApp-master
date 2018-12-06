using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events.World
{
    public class RegionResultList
    {
        [JsonProperty("region_list")]
        public List<RegionObject> region_list { get; set; }

        [JsonProperty("returned")]
        public int returned { get; set; }
        

    }
}
