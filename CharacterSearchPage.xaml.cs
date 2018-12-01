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


        private void PopulateListViewWithImages(ListView temp)
        {
            GetImages(temp);
        }


        /**
         * Method to go through each item and set the ImageSource property accoridng to FactionId
         *  maybe disable the results from being visible until this method is complete?
         */
        private void GetImages(ListView listView)
        {
            //MUST be called AFTER the fetching of the informatio is complete (after the cells are already built)

            var chars = listView.ItemsSource;
            ////determine which faction icon is used
            foreach (Character c in chars)
            {
                //if (c.FactionId == 1)  = "https://vignette.wikia.nocookie.net/planetside2/images/d/dc/Empires-tr-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021327";
                //if (c.FactionId == 2)  = "https://vignette.wikia.nocookie.net/planetside2/images/e/e1/Empires-vs-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021023";
                //if (c.FactionId == 3)  = "https://vignette.wikia.nocookie.net/planetside2/images/1/1e/Empires-nc-icon.png/revision/latest/zoom-crop/width/90/height/55?cb=20120927021335";
            }
        }

//        private ListView PopulateListView(CharacterQueryResult cqr)
        private void PopulateListView(CharacterQueryResult cqr)
        {
            resultListView.ItemsSource = cqr.Characters;
            resultListView.ItemTemplate = new DataTemplate(() =>
            {
                //create views with bindings 
                Label charName = new Label();
                charName.SetBinding(Label.TextProperty, "Name.First");

                Label charRank = new Label();
                charRank.SetBinding(Label.TextProperty, "BattleRank.Value");

                Image factionImage = new Image();
                factionImage.HeightRequest = 60;

                

                //return assembled cell

                ViewCell viewCell = new ViewCell()                //convert to an image cell later
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(3, 1, 1, 5),
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
                                    //factionImage,
                                    charName,
                                    charRank
                                }
                            }
                        }
                    }
                };
                //foreach(ViewCell v in resultListView.ItemsSource)
                //{
                //    GetImages(resultListView);
                    
                //}

                //end of viewCell construction
                return viewCell;
            }); // end of DataTemplate construction
           // return resultListView;
            

        }
    }
}