﻿using Newtonsoft.Json;
using PsApp.Gettables;
using Xamarin.Forms;

namespace PsApp
{
    public class Character
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("faction_id")]
        public int FactionId { get; set; }
        // 1 = Terran Republic
        // 2 = Vanu Sovereignity
        // 3 = New Conglomarate
        // 4 = not implemented yet, will probably be meaningless 

        [JsonProperty("battle_rank")]
        public BattleRank BattleRank { get; set; }

        [JsonProperty("profile_id")]
        public long ProfileId { get; set; }

        //tabbed pop-up menu containing a tab for Certs, Times, and BR

        public ImageSource ImageSrc
        {
            get => ImageSrc;
            private set
            {
                if (this.FactionId == 1) ImageSrc = "https://vignette.wikia.nocookie.net/planetside2/images/d/dc/Empires-tr-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021327";
                if (this.FactionId == 2) ImageSrc = "https://vignette.wikia.nocookie.net/planetside2/images/e/e1/Empires-vs-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021023";
                if (this.FactionId == 3) ImageSrc = "https://vignette.wikia.nocookie.net/planetside2/images/1/1e/Empires-nc-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021335";

            }
        }
    }
}