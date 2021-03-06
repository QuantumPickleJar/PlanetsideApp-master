﻿using System;
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
        //NOTE TO DEBUGGER:
        //SWAP THESE TWO STATEMENTS WHEN API IS WORKING FOR OUR SERVICEID
        private string QueryServiceId = "example";

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

        private async Task GetSearchResultsAsync()
        {
            PlanetsideService pService = new PlanetsideService(QueryServiceId);
            CharacterQueryResult cqr = await pService.GetMultipleCharacters(charSearch.Text.ToLower());
            PopulateListView(cqr);
            //PopulateListViewWithImages(temp);
        }


        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Pages.DetailedCharacterPage page = new Pages.DetailedCharacterPage((Character)e.Item);
            Navigation.PushAsync(page);
            //Navigation.PushModalAsync(page);
        }

        /**
         * Method to go through each item and set the ImageSource property accoridng to FactionId
         *  maybe disable the results from being visible until this method is complete?
         */
        private void PopulateListView(CharacterQueryResult cqr)
        {
            resultListView.ItemsSource = cqr.Characters;
        }
    }
}