using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PsApp.Events.World;
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
        List<RegionObject> myRegions = new List<RegionObject>();
        
        //deprecated
        public FacilityResolver(List<RegionObject> regionList)
        {
            this.myRegions = regionList;

        }

        public FacilityResolver(string ServiceId)
        {
            this.ServiceId = ServiceId;
        }

        public async Task<RegionResultList> GetListAsync()
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://census.daybreakgames.com/s:{ServiceId}/get/ps2:v2/region/?c:limit=10000&c:lang=en&c:show=region_id,zone_id,name.en";

                json = await client.DownloadStringTaskAsync(url);
            }
            RegionResultList resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<RegionResultList>(json);
            myRegions = resultList.Regions;
            return resultList;
        }
        // get all facilities, show only name, id and continent https://census.daybreakgames.com/s:PS2mobile2018/get/ps2:v2/region/?c:limit=10000&c:lang=en&c:show=region_id,zone_id,name.en


        public void SetRegionList(List<RegionObject> pass) { this.myRegions = pass; }

        public string GetFacilityNameById(string facilityId, List<RegionObject> regionList)
        {

            var item = regionList.FirstOrDefault(o => o.region_id == facilityId);
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
