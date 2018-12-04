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
        //PlanetsideService pService  
        private string QueryServiceId = "PS2mobile2018query";

        public CharacterSearchPage(string serviceId)
        {
            InitializeComponent();
            this.QueryServiceId = serviceId;
        }

        public CharacterSearchPage(string serviceId, PlanetsideService service)
        {
            InitializeComponent();
            this.QueryServiceId = serviceId;
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
        
        private async Task GetSearchResultsAsync()
        {
            PlanetsideService pService = new PlanetsideService(QueryServiceId);
            CharacterQueryResult cqr = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
//            ListView temp = PopulateListView(cqr);
            PopulateListView(cqr);

            //PopulateListViewWithImages(temp);
        }


        //private void PopulateListViewWithImages(ListView temp)
        //{
        //    GetImages(temp);
        //}


        /**
         * Method to go through each item and set the ImageSource property accoridng to FactionId
         *  maybe disable the results from being visible until this method is complete?
         */
       

//        private ListView PopulateListView(CharacterQueryResult cqr)
        private void PopulateListView(CharacterQueryResult cqr)
        {
            resultListView.ItemsSource = cqr.Characters;
      
        }
    }
}