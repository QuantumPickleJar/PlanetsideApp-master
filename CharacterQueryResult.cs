using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlanetsideApi
{
    public class CharacterQueryResult
    {
        [JsonProperty("character_list")]
        public List<Character> Characters { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }
    }
}
