using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Payloads
{
    /// <summary>
    /// {Timestamp} - {eventName.continent} {stabilizing/stabilized} [{server}]
    /// <!--use for warpgates-->
    /// </summary>
    public class FrontpageMetaPayload : FrontpagePayload { }
    
    /// <summary>
    /// {Timestamp} - {eventName.continent} {stabilizing/stabilized} [{server}]
    /// <!--use for warpgates-->
    /// </summary>
    public class FrontpageWarpgateStartPayload : FrontpagePayload{ }
    /// <summary>
    /// {Timestamp} - {eventName.continent} {stabilizing/stabilized} [{server}]
    /// <!--use for warpgates-->
    /// </summary>
    public class FrontpageWarpgateEndPayload : FrontpagePayload{ }
}

