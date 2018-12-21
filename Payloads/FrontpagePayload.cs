using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Payloads
{
    public class FrontpagePayload : CompactWorldEvent
    {
        public string continent { get; set; }
    }
}
