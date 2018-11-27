using Newtonsoft.Json;
using System.Collections.Generic;

namespace PsApp
{
    public class CharacterQueryResult
    {
        [JsonProperty("character_list")]
        public List<Character> Characters { get; set; }

        [JsonProperty("returned")]
        public int Returned { get; set; }

        public List<Name> returnedNames { get; set;}
    }
}
