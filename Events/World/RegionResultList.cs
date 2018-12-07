using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events.World
{
    public class RegionResultList
    {
        [JsonProperty("zone_list")]
        public List<Gettables.Continent> MajorRegions { get; set; }

        public int returned { get; set; }

    }
    
    
 
     //public class RegionResultList
    //{
    //    [JsonProperty("zone_id")]
    //    public int ContinentZoneId { get; set; }

    //    public string ContinentName 

    //    [JsonProperty("regions")]
    //    public List<RegionObject> Regions { get; set; }

    //    [JsonProperty("returned")]
    //    public int returned { get; set; }


    //}
}
