using Newtonsoft.Json;
using PsApp.Gettables;
using System.ComponentModel;
using Xamarin.Forms;

namespace PsApp
{
    public class Character //: INotifyPropertyChanged
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("faction_id")]
        public int FactionId
        {
            get;        
            set;
            //set
            //{
            //    FactionId = value;
            //    OnPropertyChanged("FactionId");
            //}
        }
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
            get
            {
                //if (this.FactionId == 1) return "https://vignette.wikia.nocookie.net/planetside2/images/e/e1/Empires-vs-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021023";
                //if (this.FactionId == 2) return "https://vignette.wikia.nocookie.net/planetside2/images/d/dc/Empires-tr-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021327";
                //if (this.FactionId == 3) return "https://vignette.wikia.nocookie.net/planetside2/images/1/1e/Empires-nc-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021335";


                if(Device.RuntimePlatform == Device.Android)
                if (this.FactionId == 1) return ImageSource.FromFile("vs_icon.png");
                if (this.FactionId == 2) return ImageSource.FromFile("nc_icon.png");
                if (this.FactionId == 3) return ImageSource.FromFile("tr_icon.png");

                return null;
            }
        }


        //public string Faction
        //{
        //    get => Faction;
        //    set
        //    {
        //        if (FactionId == 1) Faction = "VS";
        //        if (FactionId == 3) Faction = "TR";
        //        if (FactionId == 2) Faction = "NC";
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(string propertyName)
        //{
        //    if (propertyName != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}