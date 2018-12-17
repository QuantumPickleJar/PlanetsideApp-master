using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PsApp
{
    public class FisuService
    {
        public async Task<Gettables.FisuPopResult> GetPopulationAsync(string worldId)
        {
            string json;

            using (var client = new WebClient())
            {
                string url = $"https://ps2.fisu.pw/api/population/?world={worldId}";

                json = await client.DownloadStringTaskAsync(url);
                Console.WriteLine("\n\n" + json + "\n\n");
            }

            Gettables.FisuPopResult resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<Gettables.FisuPopResult>(json);
            return resultList;
        }

        public Gettables.FisuPopResult GetPopulation(string worldId)
        {
            string json;

            using (var client = new WebClient())
            {
                client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                                                "(compatible; MSIE 6.0; Windows NT 5.1; " +
                                                ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                string url = $"https://ps2.fisu.pw/api/population/?world={worldId}";
                client.Headers.Add("user-agent", "");
                json = client.DownloadString(url);
                Console.WriteLine("\n\n" + json + "\n\n");
            }

            Gettables.FisuPopResult resultList = Newtonsoft.Json.JsonConvert.DeserializeObject<Gettables.FisuPopResult>(json);
            return resultList;
        }
    }
}
