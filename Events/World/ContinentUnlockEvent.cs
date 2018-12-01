using System;
using System.Collections.Generic;
using System.Text;

//world-level event payload 
namespace PsApp.Events
{
    class ContinentUnlockEvent
    {
        public class Rootobject
        {
            public string event_name { get; set; }
            public string timestamp { get; set; }
            public string world_id { get; set; }
            public string zone_id { get; set; }
            public string triggering_faction { get; set; }
            public string previous_faction { get; set; }
            public string vs_population { get; set; }
            public string nc_population { get; set; }
            public string tr_population { get; set; }
            public string metagame_event_id { get; set; }
            public string event_type { get; set; }
        }

    }
}
