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
        
        const string serviceID = "PS2mobile2018";

        public MainPage()
        {
            InitializeComponent();
        }

        async private void startSubscription_Clicked(object sender, EventArgs e)
        {
            //PlanetsideService planetsideService = new PlanetsideService();
            PlanetsideService planetsideService = new PlanetsideService(serviceID);
            await planetsideService.StartAsync();
        
        }

        async void navCharacters_Clicked(object sender, EventArgs e)
        {
            CharacterSearchPage charSearch = new CharacterSearchPage(serviceID);
            await Navigation.PushAsync(charSearch);
        }
    }
}
