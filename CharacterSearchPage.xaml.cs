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
            ListView resultListView = new ListView


            {
                //resultListView.ItemsSource = cqr.Characters;
                //resultListView,ItemTemplate = new DataTemplate(() =>

                ItemsSource = cqr.Characters,
                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imgCell = new ImageCell();
                    
                    imgCell.SetBinding(ImageCell.ImageSourceProperty, "ImageSrc");
                    imgCell.SetBinding(TextCell.TextProperty, "Name.First");
                    imgCell.SetBinding(TextCell.DetailProperty, "BattleRank.Value");

                    //string s = imgCell.Detail;
                    //if int()

                    //Image factionImage = new Image();
                    //                    factionImage.SetBinding(Image.SourceProperty, "ImageSrc");

                    return imgCell;
                })
            };
                        

            //end delegate
            this.Content = new StackLayout
            {
                Children =
                {
                    charSearch,
                    resultListView
                }
            };
        }
        
    }
}