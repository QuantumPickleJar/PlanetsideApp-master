using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{

    //zone ids
    //2 = Indar
    //4 = Hossin
    //6 = Amerish
    //8 = Esamir
    // class (maybe make into an interface??) to get the name of a facility, given the facilityId from the planetside service
    
    public class FacilityResolver
    {
        List<Events.World.RegionObject> myRegions = new List<Events.World.RegionObject>();

        public FacilityResolver(List<Events.World.RegionObject> regionList)
        {
            this.myRegions = regionList;

        }
        
        // get all facilities, show only name, id and continent https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/region/?c:limit=10000&c:lang=en&c:show=region_id,zone_id,name.en


        public string GetFacilityNameById(string facilityId)
        {
            var placeholder = new Events.World.RegionObject();
            string name = string.Empty;
            foreach (var region in myRegions)
            {
                if (region.region_id == facilityId)
                {
                    placeholder = region;
                }
            }
            if (placeholder.name != null) return placeholder.name.en;
            else return "no match";   
        }
    }
}
