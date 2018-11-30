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
        //PlanetsideService pService; 
        private string query;
        public string ServiceId { get; }

        public CharacterSearchPage(string serviceId)
        {
            InitializeComponent();
            this.ServiceId = serviceId;
        }

        public CharacterSearchPage(string serviceId, PlanetsideService service)
        {
            InitializeComponent();
            this.ServiceId = serviceId;
            //pService = service; 
        }

        private async void charSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            //query = charSearch.Text.ToLower();
            await GetSearchResultsAsync();
            //GetSearchResultSingle();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void GetSearchResultSingle()
        {
            PlanetsideService service = new PlanetsideService(ServiceId);
            this.BindingContext = service.GetCharacter(charSearch.Text.ToLower());
        }

        private async Task GetSearchResultsAsync()
        {
            PlanetsideService pService = new PlanetsideService(ServiceId);
            //this.BindingContext = pService.GetMultipleCharacters(query);
            //this.BindingContext = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
            CharacterQueryResult cqr = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
            PopulateListView(cqr);
        }

        private void PopulateListView(CharacterQueryResult cqr)
        {
            resultListView.ItemsSource = cqr.Characters;
            resultListView.ItemTemplate = new DataTemplate(() =>
            {
                //create views with bindings 
                Label charName = new Label();
                charName.SetBinding(Label.TextProperty, "Name.First");

                Label charRank = new Label();
                charRank.SetBinding(Label.TextProperty, "BattleRank");

                //Image factionImage = new Image();
                ////determine which faction icon is used
                //foreach (Character c in cqr)
                //{
                //    if (c.FactionId == 1) factionImage.Source = "https://vignette.wikia.nocookie.net/planetside2/images/d/dc/Empires-tr-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021327";
                //    if (c.FactionId == 2) factionImage.Source = "https://vignette.wikia.nocookie.net/planetside2/images/e/e1/Empires-vs-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021023";
                //    if (c.FactionId == 3) factionImage.Source = "https://vignette.wikia.nocookie.net/planetside2/images/1/1e/Empires-nc-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021335";
                //}

                //return assembled cell

                return new ViewCell                //convert to an image cell later
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            //factionImage,
                            new StackLayout
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Spacing = 0,
                                Children =
                                {
                                    charName,
                                    charRank
                                }
                            }
                        }
                    }
                };
            });
        }
    }
}