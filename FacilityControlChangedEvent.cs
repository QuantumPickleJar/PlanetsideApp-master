using System;
using System.Collections.Generic;
using System.Text;

//world-level event payload
namespace PsApp.Events
{
    class FacilityControlChangedEvent
    {
        public class Rootobject
        {
            public string event_name { get; set; }
            public string timestamp { get; set; }
            public string world_id { get; set; }
            public string old_faction_id { get; set; }
            public string outfit_id { get; set; }
            public string new_faction_id { get; set; }
            public string facility_id { get; set; }
            public string duration_held { get; set; }
            public string zone_id { get; set; }
        }

    }
}
