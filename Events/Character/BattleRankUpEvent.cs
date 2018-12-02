using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events.Character
{
    class BattleRankUpEvent : Payload
    {   
        public class RootObject
        {
            [JsonProperty("character_id")]
            public string Character_id { get; set; }
            [JsonProperty("battle_rank")]
            public string Battle_Rank { get; set; }

        }

    }
}
