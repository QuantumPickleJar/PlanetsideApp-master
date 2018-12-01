using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events
{
    

    //class designed to simplify the serialization of a command message that we are going to send UP to the WebSocket
    //takes in 
    
    public class Command
    {
        //this is what Newtonsoft.json will serialize
        public string serializedMessage { get; private set; }


        public string myAction, myService;
        public string[] characters, worlds, eventNames;
        //
        //worlds self explanatory
        //example eventNames: ["FacilityControl","MetagameEvent"]

        /// <summary>
        /// Command is the message sent to the WebSocket telling it what we want it to do 
        /// </summary>    
        /// <param name="action">the action we want to be performed (subscribe, clearSubscribe, recentCharacterIds,recentCharacterIdsCount</param>
        /// <param name="service">performing action to this.  Ex: SUBSCRIBE to EVENT </param>
        public Command(string action, string service)
        {
            myAction = action;
            myService = service;
        }

        /// <summary>
        /// Command is the message sent to the WebSocket telling it what we want it to do 
        /// </summary>    
        /// <param name="action">the action we want to be performed (subscribe, clearSubscribe, recentCharacterIds,recentCharacterIdsCount</param>
        /// <param name="service">performing action to this.  Ex: SUBSCRIBE to EVENT </param>
        public Command(string action, string service, string[] characters)
        {
            myAction = action;
            myService = service;
        }


    }

}
