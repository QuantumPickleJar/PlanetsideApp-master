using PsApp.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp
{

    /// <summary>
    /// Class to hold a multipurpose payload in a format easier for the ListView to access 
    /// </summary>
    
    public class VisualEventPayload : VisualPayload
    {
       
        //public Payload payload;
        //public int event_id { get; set; }
        //public string eventCont { get; private set; }
        //public string eventStatus { get; set; }
        //public string eventName
        //{
        //    //separate continent un/lockings when i add ContinentLockEventHandlers
        //    get
        //    {
        //        if (event_id == 9)
        //        { eventCont = "Amerish"; return "Power Rush"; }

        //        if (event_id == 14)
        //        { eventCont = "Esamir"; return "Power Rush"; }

        //        if (event_id == 16)
        //        { eventCont = "Hossin"; return "Power Rush"; }

        //        if (event_id == 151)
        //        { eventCont = "Esamir"; return "Continent Locked"; }

        //        if (event_id == 152)
        //        { eventCont = "Esamir"; return "Continent Alert"; }

        //        if (event_id == 158)
        //        { eventCont = "Amerish"; return "Continent Alert"; }

        //        if (event_id == 159)
        //        { eventCont = "Amerish"; return "Warpgate"; }

        //        if (event_id == 160)
        //        { eventCont = "Esamir"; return "Warpgate"; }

        //        if (event_id == 161)
        //        { eventCont = "Indar"; return "Warpgate"; }

        //        if (event_id == 162)
        //        { eventCont = "Hossin"; return "Warpgate"; }



        //        if (event_id == 167)
        //        { eventCont = "Indar?"; return "Aerial Anomaly(?)"; }

        //        if (event_id == 172)
        //        { eventCont = "Amerish"; return "Aerial Anomaly"; }

        //        if (event_id == 173)
        //        { eventCont = "Esamir"; return "Aerial Anomaly"; }

        //        if (event_id == 174)
        //        { eventCont = "Hossin"; return "Aerial Anomaly"; }


        //        if (event_id == 180)
        //        { eventCont = "Indar"; return "Gaining Ground"; }

        //        if (event_id == 182)
        //        { eventCont = "Amerish"; return "Gaining Ground"; }

        //        if (event_id == 183)
        //        { eventCont = "Hossin"; return "Gaining Ground"; }
        //        return $"UNKNOWN EVENT ID: {event_id}*";
        //    }

        //}
        //public double nc { get; set; }
        //public double tr { get; set; }
        //public double vs { get; set; }

    

        //public string name { get; set; }
        //public string zoneId { get; set; }
        //public string continent { get; set; }
        //public string facilityAction
        //{
        //    get
        //    {
        //        if (payload.new_faction_id == payload.old_faction_id) return "defended";
        //        else return "captured";
        //        //probably have to define a different viewcell for when we receive this type message,
        //        //meaning we probaly have to make the default one so we are able to switch back and forth as needed
        //    }
        //}
    }
}
