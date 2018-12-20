using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events.World
{
    public class EventDataclass
    {
        Event[] theList =
            {
                new Event{event_name = "Dome Domination", continent = "Amerish", event_id = 7},
                new Event{event_name = "Dome Domination", continent = "Indar", event_id = 10},
                new Event{event_name = "Dome Domination", continent = "Esamir", event_id = 13},
                new Event{event_name = "Dome Domination", continent = "Hossin", event_id = 16},

                new Event{event_name = "Technological Advancement", continent = "Amerish", event_id = 8},
                new Event{event_name = "Technological Advancement", continent = "Indar", event_id = 11},
                new Event{event_name = "Technological Advancement", continent = "Esamir", event_id = 164},
                new Event{event_name = "Technological Advancement", continent = "Hossin", event_id = 18},

                new Event{event_name = "Power Rush", continent = "Amerish", event_id = 9},
                new Event{event_name = "Power Rush", continent = "Indar",  event_id = 12},
                new Event{event_name = "Power Rush", continent = "Esamir", event_id = 14},
                new Event{event_name = "Power Rush", continent = "Hossin", event_id = 18},

                new Event{event_name = "Indar Superiority", continent = "Indar", event_id = 123},
                new Event{event_name = "Indar Superiority", continent = "Indar", event_id = 147},
                new Event{event_name = "Indar Enlightenment", continent = "Indar", event_id = 124},
                new Event{event_name = "Indar Enlightenment", continent = "Indar", event_id = 148},
                new Event{event_name = "Indar Liberation", continent = "Indar", event_id = 125},
                new Event{event_name = "Indar Liberation", continent = "Indar", event_id = 149},

                new Event{event_name = "Esamir Superiority", continent = "Esamir", event_id = 126},
                new Event{event_name = "Esamir Superiority", continent = "Esamir", event_id = 150},
                new Event{event_name = "Esamir Enlightenment", continent = "Esamir", event_id = 127},
                new Event{event_name = "Esamir Enlightenment", continent = "Esamir", event_id = 151},
                new Event{event_name = "Esamir Liberation", continent = "Esamir", event_id = 128},
                new Event{event_name = "Esamir Liberation", continent = "Esamir", event_id = 152},

                new Event{event_name = "Hossin Superiority", continent = "Hossin", event_id = 129},
                new Event{event_name = "Hossin Superiority", continent = "Hossin", event_id = 153},
                new Event{event_name = "Hossin Enlightenment", continent = "Hossin", event_id = 130},
                new Event{event_name = "Hossin Enlightenment", continent = "Hossin", event_id = 154},
                new Event{event_name = "Hossin Liberation", continent = "Hossin", event_id = 131},
                new Event{event_name = "Hossin Liberation", continent = "Hossin", event_id = 155},

                new Event{event_name = "Amerish Superiority", continent = "Amerish", event_id = 132},
                new Event{event_name = "Amerish Superiority", continent = "Amerish", event_id = 156},
                new Event{event_name = "Amerish Enlightenment", continent = "Amerish", event_id = 133},
                new Event{event_name = "Amerish Enlightenment", continent = "Amerish", event_id = 157},
                new Event{event_name = "Amerish Liberation", continent = "Amerish", event_id = 134},
                new Event{event_name = "Amerish Liberation", continent = "Amerish", event_id = 158},

                new Event{event_name = "Amerish Warpgates Stabilizing", continent = "Amerish", event_id=159},
                new Event{event_name = "Esamir Warpgates Stabilizing", continent = "Esamir", event_id = 160},
                new Event{event_name = "Indar Warpgates Stabilizing", continent = "Indar", event_id =   162},
                new Event{event_name = "Hossin Warpgates Stabilizing", continent = "Hossin", event_id = 163},

                new Event{event_name = "Aerial Anomalies", continent = "Indar", event_id = 167},
                new Event{event_name = "Aerial Anomalies", continent = "Amerish", event_id = 172},
                new Event{event_name = "Aerial Anomalies", continent = "Esamir", event_id = 173},
                new Event{event_name = "Aerial Anomalies", continent = "Hossin", event_id = 174},

                new Event{event_name = "Esamir Unstable Meltdown", continent = "Esamir", event_id = 176},
                new Event{event_name = "Esamir Unstable Meltdown", continent = "Esamir", event_id = 186},
                new Event{event_name = "Esamir Unstable Meltdown", continent = "Esamir", event_id = 190},

                new Event{event_name = "Hossin Unstable Meltdown", continent = "Hossin", event_id = 177},
                new Event{event_name = "Hossin Unstable Meltdown", continent = "Hossin", event_id = 187},
                new Event{event_name = "Hossin Unstable Meltdown", continent = "Hossin", event_id = 191},

                new Event{event_name = "Amerish Unstable Meltdown", continent = "Amerish", event_id = 178},
                new Event{event_name = "Amerish Unstable Meltdown", continent = "Amerish", event_id = 188},
                new Event{event_name = "Amerish Unstable Meltdown", continent = "Amerish", event_id = 192},

                new Event{event_name = "Indar Unstable Meltdown", continent = "Indar", event_id = 179},
                new Event{event_name = "Indar Unstable Meltdown", continent = "Indar", event_id = 189},
                new Event{event_name = "Indar Unstable Meltdown", continent = "Indar", event_id = 193},

                new Event{event_name = "Gaining Ground", continent = "Indar", event_id = 180},
                new Event{event_name = "Gaining Ground", continent = "Esamir", event_id = 181},
                new Event{event_name = "Gaining Ground", continent = "Amerish", event_id = 182},
                new Event{event_name = "Gaining Ground", continent = "Hossin", event_id = 183}
            };

        public Event[] GetEvents()
        {
            return theList;
        }
        //public Event MatchEvents(MetaGame
    }

    public class Event
    {
        public string event_name { get; set; }
        public string continent { get; set; }
        public int continent_id
        {
            get
            {
                return GetContinentName(continent);
            }
        }
        public int event_id { get; set; }

        public int GetContinentName(string name)
        {
            if (name == "Indar") return 2;
            if (name == "Hossin") return 4;
            if (name == "Amerish") return 6;
            if (name == "Esamir") return 8;
            else
            {

                Console.WriteLine("INVALID CONTINENT NAME");
                return 0;
            }
        }
    }
}

