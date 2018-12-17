using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events
{
    public class WorldEventListResult
    {
        public List<World_Event> world_event_list { get; set; }
        public int returned { get; set; }
        //todo: add GetEnumerator
    }
}
