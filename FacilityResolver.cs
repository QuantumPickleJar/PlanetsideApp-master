using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{
    
    // class (maybe make into an interface??) to get the name of a facility, given the facilityId from the planetside service
    
    public class FacilityResolver
    {
        private string FACILITY_ID { get; set; }

        public FacilityResolver(string facilityId)
        {
            this.FACILITY_ID = facilityId;
        }
        //https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/zone/?c:join=map_region^list:1^inject_at:regions^hide:zone_id%28map_hex^list:1^inject_at:hex^hide:zone_id%27map_region_id%29&c:tree=start:regions^field:facility_type^list:1&c:lang=en&c:limit=10
        // 7 = warpgate
        // 
        // the paramter facility_type_id is probably going to be extremely useful in filtering

        // get all facilities, show only name, id and continent https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/region/?c:limit=10000&c:lang=en&c:show=region_id,zone_id,name.enen

        string GetFacilityNameById(string facilityID)
        {
            return null;   
        }
    }
}
