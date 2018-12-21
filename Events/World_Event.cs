using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    public class World_Event
    {

        public string facility_id { get; set; }
        public string faction_old { get; set; }
        public string faction_new { get; set; }
        public string duration_held { get; set; }
        public string objective_id { get; set; }
        public long timestamp { get; set; }
        public string zone_id { get; set; }
        public string world_id { get; set; }
        public string event_type { get; set; }
        public string table_type { get; set; }
        public string outfit_id { get; set; }
        public string metagame_event_id { get; set; }
        public string metagame_event_state { get; set; }
        public string faction_nc { get; set; }
        public string faction_tr { get; set; }
        public string faction_vs { get; set; }
        public string experience_bonus { get; set; }
        public string instance_id { get; set; }
        public string metagame_event_state_name { get; set; }
    }
}