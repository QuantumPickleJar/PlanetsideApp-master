using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{
    public class Certs
    {
        [JsonProperty("earned_points")]
        public int TotalCerts { get; set; }
        
        public string gifted_points { get; set; }

        [JsonProperty("spent_points")]
        public int SpentCerts{ get; set; }

        [JsonProperty("available_points")]
        public int PointsBalance{ get; set; }

        public string percent_to_next { get; set; }
    }
}
