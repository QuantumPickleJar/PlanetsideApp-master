using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
namespace PsApp
{
    public class AFKJob
    {
        public AFKJob(long unix)
        {
            string uri = $"https://census.daybreakgames.com/get/ps2:v2/world_event/?world_id=17&after={unix}&c:limit=50";
        }

        public AFKJob(long unix, int worldId)
        {
            string uri = $"https://census.daybreakgames.com/get/ps2:v2/world_event/?world_id={worldId}&after={unix}&c:limit=50";
        }
    }
}
