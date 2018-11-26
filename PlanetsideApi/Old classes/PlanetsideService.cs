using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
namespace PlanetsideApp.Events
{    // tabbed pages?
     // learn how to use websocket
     //
     // subscribed events updates:
     // default every 60 seconds, 
     // have a method somewhere that allows user to force a refresh no more than 2 times a second, max 3 times every 5 seconds;

    //https://census.daybreakgames.com/#ps2-websocket1
    // classes: mapHandler - maybe
    //          killFeed    
    //          mapFeed
    //          alertFeed
    //              
    // settings page that allows selection of one of four themes from a drop down menu

    // this class is where all of the websocket information should be
    //also extracts the json and turns it into readable data.


    public class PlanetsideService
    {
        public PlanetsideService(String serviceID)
        {
            //const string SERVICE_ID = serviceID;
        }

    }

    public class CharacterQueryResult
    {
        [Newtonsoft.Json.JsonProperty("character_list")]
        public List<Character> Characters { get; set; };
    }

    public class Character
    {
        string json;



        using (var client = new WebClient())
        {




        //[Newtonsoft.Json.JsonProperty("first")]
        //public string First { get; set; }

        //[Newtonsoft.Json.JsonProperty("faction_id")]
        //public long FactionId { get; set; }

        //[Newtonsoft.Json.JsonProperty("battle_rank")]
        //public BattleRank battleRank { get; set; }

        
    }




    //public event FacilityControl
    ///we'll need our own event args
    //Event Handler

    //
    //public event FacilityControlChange()
    //{
        
    //}
}
    