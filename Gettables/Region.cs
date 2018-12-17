using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Gettables
{
    public class Region
    {
        public class Rootobject
        {
            public Region_List[] region_list { get; set; }
            public int returned { get; set; }
        }

        public class Region_List
        {
            public string region_id { get; set; }
            public string zone_id { get; set; }
            public Name name { get; set; }
        }

        public class Name
        {
            public string en { get; set; }
        }

    }
}
