using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsApp
{

    //listview that displays object of type Event
   
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedPage : ContentPage
	{
        private bool _IsStartButtonRunning = false;
        const string serviceID = "PS2mobile2018";

        List<string> socketOutput = new List<string>();
        PlanetsideService planetsideService;
        


		public FeedPage ()
		{
			InitializeComponent ();
            planetsideService = new PlanetsideService(serviceID);

            planetsideService.FacilityControlChanged += PlanetsideService_FacilityControlChanged;
            consoleOut.ItemsSource = facilityControlMessages;
        }

        ObservableCollection<string> facilityControlMessages = new ObservableCollection<string>();

        private void PlanetsideService_FacilityControlChanged(object sender, FacilityControlChangedEventArgs e)
        {
            // change this string to something more meaningful
            facilityControlMessages.Add($"Facility Control Changed : {e.Payload.facility_id}");
        }

        private async void startSubscription_Clicked(object sender, EventArgs e)
         //add some sort of failsafe (probably a bool value) 
        {
            if(_IsStartButtonRunning==false)
            {
                _IsStartButtonRunning = true;
                startSubscription.IsEnabled = false;
                //PlanetsideService planetsideService = new PlanetsideService();

                await planetsideService.StartAsync();

            }
            //these might be frivolous 
            _IsStartButtonRunning = false;
            startSubscription.IsEnabled = true;
        }

        //problem: need to call StartAsync to fill the socketOutput list, 
        //but can't figure out how to make them call in the correct order. 

      

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