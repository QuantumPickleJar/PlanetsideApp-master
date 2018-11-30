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
        private string query;
        public string ServiceId { get; }

        public CharacterSearchPage(string serviceId)
		{
			InitializeComponent ();
            this.ServiceId = serviceId;
        }

        private void charSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            PlanetsideService service = new PlanetsideService("PS2mobile2018");
            service.GetCharacter(query);

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
            this.BindingContext = pService.GetMultipleCharacters(query);
        }
    }
}