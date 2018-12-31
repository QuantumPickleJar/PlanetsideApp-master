using System.ComponentModel;

namespace PsApp.Pages
{
    public class DetailedCharacterPageViewModel : INotifyPropertyChanged
    {
        string battleRankValue, nameFirst, availableCerts, spentCerts, totalCerts;
        //int faction_Id, character_id;

        public string BattleRank
        {
            get { return battleRankValue; }
            set
            {
                if (battleRankValue != value)
                {
                    battleRankValue = value;
                    OnPropertyChanged("BattleRankValue");
                }
            }
        }

        public string NameFirst
        {
            get { return nameFirst; }
            set
            {
                if (nameFirst != value)
                {
                    nameFirst = value;
                    OnPropertyChanged("NameFirst");
                }
            }
        }
        public string AvailableCerts
        {
            get { return availableCerts; }
            set
            {
                if (availableCerts != value)
                {
                    availableCerts = value;
                    OnPropertyChanged("AvailableCerts");
                }
            }
        }

        //public int FactionId
        //{
        //    get { return faction_Id; }
        //    set
        //    {
        //        if (faction_Id!= value)
        //        {
        //            faction_Id = value;
        //            OnPropertyChanged("FactionId");
        //        }
        //    }
        //}

        //public int CharacterId
        //{
        //    get { return character_id; }
        //    set
        //    {
        //        if (character_id!= value)
        //        {
        //            character_id = value;
        //            OnPropertyChanged("CharacterId");
        //        }
        //    }
        //}

        public string SpentCerts
        {
            get { return spentCerts; }
            set
            {
                if (spentCerts != value)
                {
                    spentCerts = value;
                    OnPropertyChanged("SpentCerts");
                }
            }
        }

        public string TotalCerts
        {
            get { return totalCerts; }
            set
            {
                if (totalCerts != value)
                {
                    totalCerts = value;
                    OnPropertyChanged("TotalCerts");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}