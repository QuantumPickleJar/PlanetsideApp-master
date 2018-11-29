using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterSearchPage : ContentPage
	{
        private bool _isLoading;
        
        public string ServiceId { get; }

        public CharacterSearchPage(string serviceId)
		{
			InitializeComponent ();
            this.ServiceId = serviceId;
        }

        private async void charSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            await GetSearchResultsAsync();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async Task GetSearchResultsAsync()
        {
            PlanetsideService pService = new PlanetsideService(ServiceId);
            //pService.GetCharacter(PlanetsideService.characterId);
            this.BindingContext = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
        }

        private async Task GetSearchSingleResultAsync()
        {
            PlanetsideService pService = new PlanetsideService(ServiceId);
            //pService.GetCharacter(PlanetsideService.characterId);
            this.BindingContext = pService.GetCharacter(charSearch.Text.ToLower());
        }

        private void resultListView_Unfocused(object sender, FocusEventArgs e)
        {
            this.resultListView.SelectedItem = null;
        }

        private void debugButton_Clicked(object sender, EventArgs e)
        {
            GetSearchSingleResultAsync();
        }
        //overloaded methods for making a buffer Object of type Buffer that can be deserialized to be read as the correct command string since we can't set it directly 
        //overloaded methods for making a buffer Object of type Buffer that can be deserialized to be read as the correct command string since we can't set it directly 
    }
}