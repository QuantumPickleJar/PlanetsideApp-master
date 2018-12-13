using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPageMaster : ContentPage
    {
        public ListView ListView;

        public SearchPageMaster()
        {
            InitializeComponent();

            BindingContext = new SearchPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class SearchPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<SearchPageMenuItem> MenuItems { get; set; }
            
            public SearchPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<SearchPageMenuItem>(new[]
                {
                    new SearchPageMenuItem { Id = 0, Title = "Page 1" },
                    new SearchPageMenuItem { Id = 1, Title = "Page 2" },
                    new SearchPageMenuItem { Id = 2, Title = "Page 3" },
                    new SearchPageMenuItem { Id = 3, Title = "Page 4" },
                    new SearchPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}