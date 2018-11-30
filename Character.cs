using Newtonsoft.Json;
using PsApp.Gettables;

namespace PsApp
{
    public class Character
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("faction_id")]
        public long FactionId { get; set; }
        // 1 = Terran Republic
        // 2 = Vanu Sovereignity
        // 3 = New Conglomarate
        // 4 = not implemented yet, will probably be meaningless 

        [JsonProperty("battle_rank")]
        public BattleRank BattleRank { get; set; }

        [JsonProperty("profile_id")]
        public long ProfileId { get; set; }
        
        //tabbed pop-up menu containing a tab for Certs, Times, and BR
    }
}