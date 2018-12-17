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
	public partial class FeedPageSelect : ContentPage
	{

		public FeedPageSelect ()
		{
			InitializeComponent();
		}
        private async void server1_Clicked(object sender, EventArgs e)
        {
            //Connery 1 
            FeedPage feedPage = new FeedPage(1);
            await Navigation.PushAsync(feedPage);
        }
        private async void server2_Clicked(object sender, EventArgs e)
        {
            //Miller 10
            FeedPage feedPage = new FeedPage(10);
            await Navigation.PushAsync(feedPage);
        }

        private async void server3_Clicked(object sender, EventArgs e)
        {
            //Cobalt 13
            FeedPage feedPage = new FeedPage(13);
            await Navigation.PushAsync(feedPage);
        }

        private async void server4_Clicked(object sender, EventArgs e)
        {
            //Emerald 17
            FeedPage feedPage = new FeedPage(17);
            await Navigation.PushAsync(feedPage);
        }

        private async void server5_Clicked(object sender, EventArgs e)
        {
            //Jaeger 19
            FeedPage feedPage = new FeedPage(19);
            await Navigation.PushAsync(feedPage);
        }

        private async void server6_Clicked(object sender, EventArgs e)
        {
            //Briggs 25
            FeedPage feedPage = new FeedPage(25);
            await Navigation.PushAsync(feedPage);
        }
        private async void server7_Clicked(object sender, EventArgs e)
        {
            //SolTech 40
            FeedPage feedPage = new FeedPage(40);
            await Navigation.PushAsync(feedPage);
        }
    }
}