using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PsApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        string localTime, prefServer;
        public string LocalTime
        {
            get { return localTime; }
            set
            {
                if (localTime != value)
                {
                    localTime = value;
                    OnPropertyChanged("LocalTime");
                }
            }
        }

        public string PrefServer
        { 
            get { return prefServer; }
            set
            {
                if (prefServer != value)
                {
                    prefServer = value;
                    OnPropertyChanged("PrefServer");
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
