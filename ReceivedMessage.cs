using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events
{
    class ReceivedMsg : Message
    {
        /// <summary>
        /// A ReceivedMsg contains a payload.  Payloads are RECEIVE-ONLY
        /// 
        /// </summary>
        [JsonProperty("payload")]
        public Payload newPayload { get; set; }

        //
        //{
        //    "detail":"EventServerEndpoint_Connery_1",
        //    "online":"true",
        //    "service":"event",
        //    "type":"serviceStateChanged"
        //}


        /// <summary>
        /// type of service that we are doing.  
        /// Subscribe
        /// </summary>
        /// <remarks>This property will likely be used for filtering </remarks>
        /// 
        //[JsonProperty("service")]
        //public string Service { get; set; }

        ////probably going to only use for filtering
        //[JsonProperty("type")]
        //public string Type { get; set; }

        //{   "connected":"true",
        //    "service":"push",
        //    "type":"connectionStateChanged"
        //}

    }
}
