using System;
using System.Collections.Generic;
using System.Text;

//world-level event payload 
namespace PsApp.Events
{
    class MetagameEventEvent
    {
        public class Rootobject
        {
            public string event_name { get; set; }
            public string timestamp { get; set; }
            public string world_id { get; set; }
            public string experience_bonus { get; set; }
            public string faction_nc { get; set; }
            public string faction_tr { get; set; }
            public string faction_vs { get; set; }
            public string metagame_event_id { get; set; }
            public string metagame_event_state { get; set; }
            public string zone_id { get; set; }
        }

    }
}
