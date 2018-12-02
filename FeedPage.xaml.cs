using System;
using System.Collections.Generic;
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
        const string serviceID = "PS2mobile2018";

        List<string> socketOutput = new List<string>();
        PlanetsideService planetsideService;

		public FeedPage ()
		{
			InitializeComponent ();
            planetsideService = new PlanetsideService(serviceID);

        }

        private void startSubscription_Clicked(object sender, EventArgs e)
        {
            //PlanetsideService planetsideService = new PlanetsideService();

            await planetsideService.StartAsync();
            new Thread(async() => 
            {
                Thread.CurrentThread.IsBackground = true;
                //while (planetsideService.IsStarted)
            //    {
            //        PopulateList();
            //    }
            }).Start();
            //if(planetsideService.CompletedTask())
            //{
            //    PopulateList();
            //}

        }

        //problem: need to call StartAsync to fill the socketOutput list, but can't figure out how to make them call in the correct order. 


        public void PopulateList()
        {
            List<Events.Payload> payloads = new List<Events.Payload>();
            socketOutput = planetsideService.GetReturnList();

            

            foreach (string s in socketOutput)
            {
                var myclass = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(s);

                payloads.Add(JsonConvert.DeserializeObject<Events.Payload>(s));
            }

            consoleOut.ItemsSource = payloads;
            //the itemsource needs to be set to an array of Event objects. 
            //consoleOut.ItemsSource = socketOutput;
            consoleOut.ItemTemplate = new DataTemplate(() =>
                {
                    //consoleOut.SetBinding(Label.TextProperty, socketOutput.Last());
                    //TextCell textCell = new TextCell()
                    //{
                    //    //debug: we want to display the serialized Payload 
                    //    Text = socketOutput.Last()
                    //};
                    //return textCell;
                    Label serv = new Label();
                    serv.SetBinding(Label.TextProperty, "service");

                    Label eventname = new Label();
                    eventname.SetBinding(Label.TextProperty, "event_name");

                    Label worldid = new Label();
                    worldid.SetBinding(Label.TextProperty, "world_id");

                    Label timestamp = new Label();
                    timestamp.SetBinding(Label.TextProperty, "timestamp");

                    ViewCell viewCell = new ViewCell()
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(5, 1, 5, 1),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                            {
                                serv,
                                eventname,
                                worldid
                                //,timestamp
                               
                            }

                        }
                    };
                    return viewCell;
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