using PsApp.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{
    /// <summary>
    /// Class to hold a multipurpose payload in a format easier for the ListView to access 
    /// </summary>
    public class VisualPayload
    {
        public Payload payload { get; set; }
        public int event_id { get; set; }
        public string eventStatus { get; set; }
        public string eventName { get; set; }
        public double nc { get; set; }
        public double tr { get; set; }
        public double vs { get; set; }

        public int faction_vs_int { get { return (int)(vs); } }
        public int faction_nc_int { get { return (int)(nc); } }
        public int faction_tr_int { get { return (int)(tr); } }
        public string name { get; set; }
        public string zoneId { get; set; }
        public string continent { get; set; }
        public string facilityAction
        {
            get
            {
                if (payload.new_faction_id == payload.old_faction_id) return "defended";
                else return "captured";
                //probably have to define a different viewcell for when we receive this type message,
                //meaning we probaly have to make the default one so we are able to switch back and forth as needed
            }
        }
    }
}
