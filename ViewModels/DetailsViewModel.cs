using System.ComponentModel;

namespace PsApp.Pages
{ 
    public class DetailsViewModel : INotifyPropertyChanged
    {

        string vs, tr, nc;
        public string Vs
        {
            get
            {
                return vs;
            }
            set
            {
                if (vs != value)
                {
                    vs = value;
                    OnPropertyChanged("Vs");
                }
            }
        }
        public string Tr
        {
            get
            {
                return tr;
            }
            set
            {
                if (tr != value)
                {
                    tr = value;
                    OnPropertyChanged("Tr");
                }
            }
        }
        public string Nc
        {
            get
            {
                return nc;
            }
            set
            {
                if (nc != value)
                {
                    nc = value;
                    OnPropertyChanged("Nc");
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}