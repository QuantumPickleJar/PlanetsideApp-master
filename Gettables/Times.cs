using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{
    public class Times
    {
        [JsonProperty("creation")]
        public long CreationDateUnix { get; set; }

        [JsonProperty("creation_date")]
        public string CreationDate { get; set; }

        [JsonProperty("last_save")]
        public long LatestSaveUnix { get; set; }

        [JsonProperty("last_save_date")]
        public string LatestSaveDate { get; set; }

        [JsonProperty("last_login")]
        public long LatestLoginUnix { get; set; }

        [JsonProperty("last_login_date")]
        public string LatestLoginDate { get; set; }

        [JsonProperty("login_count")]
        public int NumLogins { get; set; }

        [JsonProperty("minutes_played")]
        public int NumMinutes { get; set; }

    }
}
