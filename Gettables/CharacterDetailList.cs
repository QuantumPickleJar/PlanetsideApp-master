using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{
    public class CharacterDetailList
    {
        //might be able to just merge this with the CharacterQueryResult 
        [JsonProperty("character_list")]
        public List<CharacterFull> CharacterResult { get; set; }
        public CharacterFull FirstCharacter
        { get
            {
                return CharacterResult[0];
            }
        }
        public int returned { get; set; }
    }
}
