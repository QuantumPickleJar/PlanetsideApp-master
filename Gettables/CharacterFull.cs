using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace PsApp.Gettables
{
    public class CharacterFull
    {
        [JsonProperty("name")]
        public Name Name { get; set; }
        public Times times { get; set; }
        public Certs certs { get; set; }
        public Stats stats { get; set; }
        public OutfitMember outfit_member { get; set; }
        public WorldIdJoinWorld world_id_join_world { get; set; }
        public BattleRank battle_rank { get; set; }
        public DailyRibbon daily_ribbon { get; set; }
        public long character_id { get; set; }
        public int faction_id { get; set; }
        public string head_id { get; set; }
        public string title_id { get; set; }
        public string profile_id { get; set; }
        public string prestige_level { get; set; }
        public string world_id { get; set; }
        public string online_status { get; set; }

        public class DailyRibbon
        {
            public string count { get; set; }
            public string time { get; set; }
            public string date { get; set; }
        }

        public class Day
        {
            public string d01 { get; set; }
            public string d02 { get; set; }
            public string d03 { get; set; }
            public string d04 { get; set; }
            public string d05 { get; set; }
            public string d06 { get; set; }
            public string d07 { get; set; }
            public string d08 { get; set; }
            public string d09 { get; set; }
            public string d10 { get; set; }
            public string d11 { get; set; }
            public string d12 { get; set; }
            public string d13 { get; set; }
            public string d14 { get; set; }
            public string d15 { get; set; }
            public string d16 { get; set; }
            public string d17 { get; set; }
            public string d18 { get; set; }
            public string d19 { get; set; }
            public string d20 { get; set; }
            public string d21 { get; set; }
            public string d22 { get; set; }
            public string d23 { get; set; }
            public string d24 { get; set; }
            public string d25 { get; set; }
            public string d26 { get; set; }
            public string d27 { get; set; }
            public string d28 { get; set; }
            public string d29 { get; set; }
            public string d30 { get; set; }
            public string d31 { get; set; }
        }

        public class Month
        {
            public string m01 { get; set; }
            public string m02 { get; set; }
            public string m03 { get; set; }
            public string m04 { get; set; }
            public string m05 { get; set; }
            public string m06 { get; set; }
            public string m07 { get; set; }
            public string m08 { get; set; }
            public string m09 { get; set; }
            public string m10 { get; set; }
            public string m11 { get; set; }
            public string m12 { get; set; }
        }

        public class Week
        {
            public string w01 { get; set; }
            public string w02 { get; set; }
            public string w03 { get; set; }
            public string w04 { get; set; }
            public string w05 { get; set; }
            public string w06 { get; set; }
            public string w07 { get; set; }
            public string w08 { get; set; }
            public string w09 { get; set; }
            public string w10 { get; set; }
            public string w11 { get; set; }
            public string w12 { get; set; }
            public string w13 { get; set; }
        }

        public class StatHistory
        {
            public string stat_name { get; set; }
            public string all_time { get; set; }
            public string one_life_max { get; set; }
            //public Day day { get; set; }
            //public Month month { get; set; }
            //public Week week { get; set; }
            public string last_save { get; set; }
            public string last_save_date { get; set; }
        }

        public class Stats
        {
            public List<StatHistory> stat_history { get; set; }
        }

        public OutfitMember myOutfit { get; set; }

        public class Name2
        {
            public string en { get; set; }
            public string de { get; set; }
            public string es { get; set; }
            public string fr { get; set; }
            public string it { get; set; }
            public string tr { get; set; }
        }

        public class WorldIdJoinWorld
        {
            public string world_id { get; set; }
            public string state { get; set; }
            public Name2 name { get; set; }
        }

        public class CharacterList
        {
            public string character_id { get; set; }
            public Name name { get; set; }
            public string faction_id { get; set; }
            public string head_id { get; set; }
            public string title_id { get; set; }
            public Times times { get; set; }
            public Certs certs { get; set; }
            public BattleRank battle_rank { get; set; }
            public string profile_id { get; set; }
            public DailyRibbon daily_ribbon { get; set; }
            public string prestige_level { get; set; }
            public string world_id { get; set; }
            public string online_status { get; set; }
            public Stats stats { get; set; }
            public OutfitMember outfit_member { get; set; }
            public WorldIdJoinWorld world_id_join_world { get; set; }
        }
    }
}
