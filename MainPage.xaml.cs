using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace PsApp
{
    public partial class MainPage : ContentPage
    {
        private bool isDev; 

        public MainPage()
        {
            InitializeComponent();
        }

        async void navCharacters_Clicked(object sender, EventArgs e)
        {
            CharacterSearchPage charSearch = new CharacterSearchPage("PS2mobile2018query");
            await Navigation.PushAsync(charSearch);
        }

        async void navLiveEvent_Clicked(object sender, EventArgs e)
        {
            FeedPage feedPage = new FeedPage();
            await Navigation.PushAsync(feedPage);
        }

        async void navSettingsPage_Clicked(object sender, EventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage(isDev);
            await Navigation.PushAsync(settingsPage);
        }
    }
}
