using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Payloads
{
    /// <summary>
    /// Used for sending string messages to the listVIew
    /// </summary>
    public class DebugPayload : FrontpagePayload
    {
        public string message { get; set; }
    }
}
