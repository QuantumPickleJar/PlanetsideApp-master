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
        List<RegionObject> AllRegions;
        //RegionObject AllRegions;
        ZoneResult Results;
         

        //deprecated
        

        public FacilityResolver(string ServiceId)
        {
            this.ServiceId = ServiceId;
        }

        public async void FillList(ZoneResult z)
        {
            
            foreach ( ZoneList item in z.zoneList)
            {
                //allZones.Add(item);
                AllRegions.AddRange(item.regions);
            }
            


            //trying to get all of the RegionObjects into the same List

            //does this work?

            //System.Collections.IList list = allZones;
            //for (int i = 0; i < list.Count; i++)
            //{
            //    List<RegionObject> r = (List<RegionObject>)list[i];

            //    //AllRegions = item.regions;
            //    AllRegions.AddRange(item.regions);
            //}
        }

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

        //it makes more sens if we have a zone_id outside of the region object so we can rule out the search results faster

        public string FetchFacilityNameFromMasterList(string FacilityId)
        {
            //get all the RegionObjects in one place

            int index = -1;
            int i;
            for (i=0;i < Results.zoneList.Count; i++)
            {
                index = Results.zoneList[i].regions.FindIndex(o => o.facility_id == FacilityId);
                if (index != -1) break;
            }

            //int index = Results.zoneList[1].regions;
            //var item = AllRegions.FirstOrDefault(o => o.facility_id == FacilityId);
            
            return Results.zoneList[i].regions[index].facility_name;

           //int index;
           // index = AllRegions.IndexOf(search);
           // if (index != null)
           //     return AllRegions.ElementAt(index).facility_name;
           // else return "no dice";
        }

        //we need to make 
        public string GetFacilityNameById(string facilityId, Continent cont)
        {

            var item = cont.regions.FirstOrDefault(o => o.facility_id == facilityId);
            //foreach (RegionObject region in regionList)
            //{
            //    if (region.region_id == facilityId)
            //    {
            //        placeholder = region;
            //    }
            //}
            if (item != null) return item.name.en;

            else return "no match";   
        }
    }
}
