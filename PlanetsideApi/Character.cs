using Newtonsoft.Json;

namespace PlanetsideApi
{
    public class Character
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("faction_id")]
        public long FactionId { get; set; }

        [JsonProperty("battle_rank")]
        public BattleRank BattleRank { get; set; }
    }
}