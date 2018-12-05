using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsApp.Events.Character
{
    class AchievementEarnedEvent : Payload.EventPayload
    {
        public class RootObject
        {
            [JsonProperty("achievement_id")]
            public int Achievement_id { get; set; }

            [JsonProperty("character_id")]
            public string Character_id { get; set; }

        }

    }
}
