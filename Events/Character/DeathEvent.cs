using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Events.Character
{
     class DeathEvent : Payload
    {

        public class RootObject
        {
            [JsonProperty("attacker_character_id")]
            public string Attacker_character_id { get; set; }

            [JsonProperty("attacker_fire_mode_id")]
            public string Attacker_fire_mode_id { get; set; }

            [JsonProperty("attacker_loadout_id")]
            public string Attacker_loadout_id { get; set; }

            [JsonProperty("attacker_vehicle_id")]
            public string Attacker_vehicle_id { get; set; }

            [JsonProperty("attacker_weapon_id")]
            public string Attacker_weapon_id { get; set; }

            [JsonProperty("character_id")]
            public string Character_id { get; set; }

            [JsonProperty("character_loadout_id")]
            public string Character_loadout_id { get; set; }

            [JsonProperty("is_critical")]
            public string Is_critical { get; set; }

            [JsonProperty("is_headshot")]
            public string Is_headshot { get; set; }

            [JsonProperty("vehicle_id")]
            public string Vehicle_id { get; set; }
        }

    }
}
