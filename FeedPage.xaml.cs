using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp
{

    //listview that displays object of type Event
   
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedPage : ContentPage
	{
        const string serviceID = "PS2mobile2018";

        List<string> socketOutput = new List<string>();
        PlanetsideService planetsideService;

		public FeedPage ()
		{
			InitializeComponent ();
            planetsideService = new PlanetsideService(serviceID);

        }

        async private void startSubscription_Clicked(object sender, EventArgs e)
        {
            //PlanetsideService planetsideService = new PlanetsideService();
            await planetsideService.StartAsync();
            while(planetsideService.IsStarted)
            {
                PopulateList();
            }

        }



        public void PopulateList()
        {

            socketOutput = planetsideService.GetReturnList();
            consoleOut.ItemsSource = socketOutput;
            consoleOut.ItemTemplate = new DataTemplate(() =>
            {
                Label line1 = new Label();
                //consoleOut.SetBinding(Label.TextProperty, socketOutput.Last());
                TextCell textCell = new TextCell()
                {
                    Text = socketOutput.Last()
                };
                return textCell;
            }
            );
        }

        async private void stopLive_Clicked(object sender, EventArgs e)
        {
            await planetsideService.StopAsync();
        }
        
        //example of what a received payload from a playerlogin event subscription would look like in json
        //    {
        //      "payload":
        //        {
        //      	"character_id":"5428057349740067905",
        //      	"event_name":"PlayerLogin",
        //      	"timestamp":"1397251287",
        //      	"world_id":"1"
        //      },
        //      "service":"event",
        //      "type":"serviceMessage"
        //    }
    }

}