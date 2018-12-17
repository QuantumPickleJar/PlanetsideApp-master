using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FisuPopPage : ContentPage
	{
        public int worldId;

		public FisuPopPage ()
		{
			InitializeComponent();
            BindingContext = new DetailsViewModel();
		}

        //private async Task GetSearchResultsAsync()
        //{
        //    FisuService f = new FisuService();
        //    Gettables.FisuPopResult list = await f.GetPopulationAsync(worldId.ToString());
        //    Console.WriteLine("\ndone list\n");
        //    Gettables.FResult result = list.result[0];
        //    //vs_label.Text = list.result[0].vs.ToString(); 
        //    //tr_label.Text = list.result[0].tr.ToString(); 
        //    //nc_label.Text = list.result[0].nc.ToString(); 
        //}
        private void GetSearchResults()
        {
            FisuService f = new FisuService();
            Gettables.FisuPopResult list = f.GetPopulation(worldId.ToString());
            Console.WriteLine("\ndone list\n");
            Gettables.FResult result = list.result[0];
            vs_label.Text = list.result[0].vs.ToString();
            tr_label.Text = list.result[0].tr.ToString();
            nc_label.Text = list.result[0].nc.ToString();
        }

        private void getPop_Clicked(object sender, EventArgs e)
        {
            if (worldId == 1 || worldId == 10 || worldId == 13 || worldId == 17 || worldId == 19 || worldId == 25 || worldId == 40)
            {
                GetSearchResults();
            }
        }

        //private async void getPop_Clicked(object sender, EventArgs e)
        //{
        //    if (worldId == 1 || worldId == 10 || worldId == 13 || worldId == 17 || worldId == 19 || worldId == 25 || worldId == 40)
        //    {
        //        await GetSearchResultsAsync();
        //    }
        //}

        private void server1_Clicked(object sender, EventArgs e)
        {
            //Connery
            worldId = 1;
        }
        
        private void server2_Clicked(object sender, EventArgs e)
        {
            //Miller
             worldId = 10;
        }
        
        private void server3_Clicked(object sender, EventArgs e)
        {
            //Cobalt
             worldId = 13;
        }
        
        private void server4_Clicked(object sender, EventArgs e)
        {
            //Emerald
             worldId = 17;
        }
        
        private void server5_Clicked(object sender, EventArgs e)
        {
            //Jaeger
            worldId = 19;
        }
        
        private void server6_Clicked(object sender, EventArgs e)
        {
            //Briggs
            worldId = 25;
        }private void server7_Clicked(object sender, EventArgs e)
        {
            //SolTech
            worldId = 40;
        }
        
    }
}