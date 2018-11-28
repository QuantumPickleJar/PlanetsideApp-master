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
            GetSearchResultsAsync();
        }

        private async Task GetSearchResultsAsync()
        {
            PlanetsideService pService = new PlanetsideService(ServiceId);
            //pService.GetCharacter(PlanetsideService.characterId);
            this.BindingContext = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
        }

        private void resultListView_Unfocused(object sender, FocusEventArgs e)
        {
            this.resultListView.SelectedItem = null;
        }
    }
}