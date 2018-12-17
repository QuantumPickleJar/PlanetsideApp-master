using PsApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace PsApp
{
    public partial class MainPage : ContentPage
    {
        public int selectedWorld = 0;

        public MainPage()
        {
            InitializeComponent();
            //navCharacters.IsEnabled = false;
            //navCont.IsEnabled = false;
            //navFisu.IsEnabled = false;
            OnCreate();
        }

        private void OnCreate()
        {
            
        }

        //async protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    //await PopulatePicker();
        //}

        //private Task PopulatePicker()
        //{
        //    //add the worlds in here
        //    //Emerald (17)
        //    //Connery (1)
        //    //Solus 
        //    //...
        //}

        async void navCharacters_Clicked(object sender, EventArgs e)
        {
            //if (selectedWorld != 0)
            {
                CharacterSearchPage charSearch = new CharacterSearchPage("PS2mobile2018query");
                await Navigation.PushAsync(charSearch);
            }
        }

        async void navLiveEvent_Clicked(object sender, EventArgs e)
        {
            FeedPage feedPage = new FeedPage(17);
            await Navigation.PushAsync(feedPage);
        }

        async void navSettingsPage_Clicked(object sender, EventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            await Navigation.PushAsync(settingsPage);
        }
        async void navSearchPage_Clicked(object sender, EventArgs e)
        {

            //SettingsPage settingsPage = new SettingsPage();
            //await Navigation.PushAsync(settingsPage);
            var notifServ = DependencyService.Resolve<INotificationService>();
            await notifServ.NotifyAsync("test title", "message message");
        }

        //void serverPicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int selectedInt = serverPicker.SelectedIndex;
        //}

        private void navCont_Clicked(object sender, EventArgs e)
        {

        }

        private async void navFisuButton_Clicked(object sender, EventArgs e)
        {
            FisuPage fisu = new FisuPage();
            await Navigation.PushAsync(fisu);
        }
        
    }
}
