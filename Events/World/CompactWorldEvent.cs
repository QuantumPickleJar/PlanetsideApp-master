using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{
    public class CompactWorldEvent
    {
        public long timestamp { get; set; }
        public string world_id { get; set; }
        public string event_type { get; set; }
        public string metagame_event_id { get; set; }
        public string metagame_event_state { get; set; }
        public int faction_nc { get; set; }
        public int faction_tr { get; set; }
        public int faction_vs { get; set; }
        public string metagame_event_state_name { get; set; }
        public string eventName { get; set; }
        public string instance_id { get; set; }
    }
}
