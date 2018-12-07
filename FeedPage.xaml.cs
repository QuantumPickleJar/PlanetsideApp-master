using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PsApp.Events;
using PsApp.Events.World;

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
        FacilityResolver fR;
        
        public FeedPage ()
		{
            fR= new FacilityResolver(serviceID);


            InitializeComponent();
            planetsideService = new PlanetsideService(serviceID);
            planetsideService.MetagameEventChange += PlanetsideService_MetagameEventChanged;
            planetsideService.FacilityControlChanged += PlanetsideService_FacilityControlChanged;
            consoleOut.ItemsSource = subscribedMessages;
        }

        ObservableCollection<string> subscribedMessages = new ObservableCollection<string>();
        private void PlanetsideService_FacilityControlChanged(object sender, FacilityControlChangedEventArgs e)
        {
            if (e != null)
            {
                //assign faction strings
                string oldFact, newFact;

                oldFact = ResolveFactionId(e.Payload.old_faction_id);
                newFact = ResolveFactionId(e.Payload.new_faction_id);

                // change this string to something more meaningful
                //subscribedMessages.Add($"Facility Control Changed : {e.Payload}");

                Facility myPlace = new Facility()
                {
                    name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                    zoneId = e.Payload.Zone_id
                };
                string action = string.Empty;
                if (e.Payload.duration_held != 0) action = "captured";
                if (e.Payload.duration_held == 0) action = "defend";

                if (myPlace.continent != "unknown continent")
                {
                    //Console.WriteLine($"\n\n{myPlace.ToString()} \nend string \n)");
                    subscribedMessages.Add($"Hex control: {newFact} has {action} {myPlace.name} \n " +
                        $"on {myPlace.continent} from {oldFact}");
               
                    //use the facility resolver class to get a better visual output
                    //subscribedMessages.Add($"Facility Control Changed : {e.Payload.facility_id}");
                }
            }
        }

        private string ResolveFactionId(int faction_id)
        {
            if (faction_id == 1) return "VS";
            if (faction_id == 2) return "TR";
            if (faction_id == 3) return "NC";
            else return "Error 500";
        }

        private void PlanetsideService_MetagameEventChanged(object sender, Events.World.MetagameEventEventArgs e)
        {
            // change this string to something more meaningful
            subscribedMessages.Add($"Metagame event : {e.Payload}");
            //use the facility resolver class to get a better visual output
            //facilityControlMessages.Add($"Facility Control Changed : {e.Payload.facility_id}");
        }


        private async void startSubscription_Clicked(object sender, EventArgs e)
         //add some sort of failsafe (probably a bool value) 
        {
            var returnedtask = await fR.GetListAsync();
            //fR.FillList(returnedtask);
            if (_IsStartButtonRunning==false)
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

        private class Facility
        {
            //    public Facility(string facilityId)
            //    {
            //        this.FacilityId = facilityId;
            //    }
            //    public string FacilityId { get; set; }

            public string name { get; set; }
            public string zoneId { get; set; }
            public string continent
            {
                get
                {
                    if (zoneId == "2") return "Indar";
                    if (zoneId == "4") return "Hossin";
                    if (zoneId == "6") return "Amerish";
                    if (zoneId == "8") return "Esamir";
                    else return "unknown continent";
                }
            }
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