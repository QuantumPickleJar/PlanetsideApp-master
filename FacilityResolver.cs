using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PsApp.Events.World;
using PsApp.Gettables;

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
        private string ServiceId;
        List<ZoneList> allZones = new List<ZoneList>();
        ZoneResult Results;


        //deprecated


        //public FacilityResolver(string ServiceId)
        //{
        //    this.ServiceId = ServiceId;
        //}

            

        public async Task<ZoneResult> GetListAsync()
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/get/ps2:v2/zone/?c:join=map_region^list:1^inject_at:regions^%28&c:tree=start:regions^&c:lang=en&c:limit=10";

                json = await client.DownloadStringTaskAsync(url);
            }
            //get list of all major regions

            ZoneResult resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<ZoneResult>(json);
            //resultList2.zone_list

            this.Results = resultList;
            return resultList;
        }
        // get all facilities, show only name, id and continent https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/region/?c:limit=10000&c:lang=en&c:show=region_id,zone_id,name.en

        public string GetContinentName(string zoneId)
        {
            if (zoneId == "2") return "Indar";
            if (zoneId == "4") return "Hossin";
            if (zoneId == "6") return "Amerish";
            if (zoneId == "8") return "Esamir";
            else return "UNKNOWN CONTINENT (zoneId)*";
        }




        public string FetchFacilityNameFromMasterList(string FacilityId)
        {
            //get all the RegionObjects in one place

            int index = -1;
            int i;

            //fetch RegionObject 
            for (i = 0; i < Results.zoneList.Count; i++)
            {
                if (Results.zoneList[i].regions != null)
                {
                    index = Results.zoneList[i].regions.FindIndex(o => o.facility_id == FacilityId);
                    if (index != -1) break;
                }
            }
            //
            if (index != -1)
            {
                if (Results.zoneList[i].regions[index].facility_name != null)
                {
                    return Results.zoneList[i].regions[index].facility_name;
                }
            }
             return "UNKNOWN FACILITY*";
            //int index;
            // index = AllRegions.IndexOf(search);
            // if (index != null)
            //     return AllRegions.ElementAt(index).facility_name;
            // else return "no dice";
        }
    }
}
