using Newtonsoft.Json;

namespace PsApp
{
    public class BattleRank
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("percent_to_next")]
        public int PercentToNext { get; set; }
    }
}