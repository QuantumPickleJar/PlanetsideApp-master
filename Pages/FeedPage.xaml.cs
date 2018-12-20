using PsApp.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace PsApp
{

    //listview that displays object of type Event

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedPage : ContentPage//, INotifyPropertyChanged 
    {
        private bool _IsStartButtonRunning = false;
        const string serviceID = "PS2mobile2018";
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        List<string> socketOutput = new List<string>();
        PlanetsideService planetsideService;
        FacilityResolver fR;

        int worldId;
        ObservableCollection<VisualPayload> subscribedMessages = new ObservableCollection<VisualPayload>();



        public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();
        
        public FeedPage(int SelectedWorld)
        {
            this.worldId = SelectedWorld;
            fR = new FacilityResolver();
            subscribedMessages.CollectionChanged += SubscribedMessages_CollectionChanged;
            InitializeComponent();
            planetsideService = new PlanetsideService(serviceID);
            planetsideService.MetagameEventChange += PlanetsideService_MetagameEventChangedAsync;
            planetsideService.ConnectionStateChanged += PlanetsideService_ConnectionStateChanged;
            planetsideService.FacilityControlChanged += PlanetsideService_FacilityControlChanged;
            consoleOut.ItemsSource = subscribedMessages;
            consoleOut.ItemAppearing += ConsoleOut_ItemAppearing;
        }

        private void ConsoleOut_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (subscribedMessages.Count > 20)
            {
                subscribedMessages.Remove(subscribedMessages[subscribedMessages.Count - (subscribedMessages.Count - 1)]);
            }
        }

        private void SubscribedMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            Console.WriteLine("\n\nCollection Changed\n\n");
            consoleOut.ScrollTo(subscribedMessages[subscribedMessages.Count - 1], Xamarin.Forms.ScrollToPosition.End, true);

            //app crashes when we add too many 
            
        }

        protected override void OnAppearing()
        {
            worldIdSpan.Text = new WorldIdToString().FetchString(worldId);

            base.OnAppearing();
            if (planetsideService.IsStarted == false)
            {
                startSubscription.IsEnabled = true;
            }

            if(planetsideService.IsStarted) stopLive.IsEnabled = false;
        }

       
        private void PlanetsideService_ConnectionStateChanged(object sender, MessageEventArgs e)
        {
            Console.WriteLine("---------------------EVENTRAISED ON RECEIVING END-----------------------------");
            if (e != null)
            {
                if (e.connectionStatus == "true")
                {
                    statusLabel.Text = "ONLINE";
                    statusLabel.TextColor = Color.Green;
                    startSubscription.IsEnabled = false;
                }

                if (e.connectionStatus == "false")
                {
                    statusLabel.Text = "OFFLINE";
                    statusLabel.TextColor = Color.Red;
                }
            }
        }


        private void PlanetsideService_FacilityControlChanged(object sender, FacilityControlChangedEventArgs e)
        {
            VisualPayload passMe = new VisualPayload();
            if (e != null && e.Payload.World_id == worldId || worldId == 100)
            {

                if (e.Payload.new_faction_id == e.Payload.old_faction_id && e.Payload.facility_id == null)
                {
                    Console.WriteLine("\n\nWARNING: UNKNOWN FACILITY PAYLOAD STYLE\n");
                    passMe = null;
                    return;
                    passMe = new VisualPayload()
                    {
                        payload = e.Payload,
                        name = "Unavailable",
                        zoneId = e.Payload.Zone_id,
                        continent = "Unavailable"

                    };
                }

                if ((e.Payload.old_faction_id != e.Payload.new_faction_id) && e.Payload.old_faction_id != 0)
                {
                    //it's a capture payload
                    passMe = new VisualCapturePayload()
                    {
                        payload = e.Payload,
                        name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                        zoneId = e.Payload.Zone_id,
                        continent = fR.GetContinentName(e.Payload.Zone_id)
                    };
                    Console.WriteLine("[][] DEBUG [][] CAPTURE passMe param:  " + passMe.continent + " / " + passMe.name + " / " + passMe.payload.new_faction_id + " from " + passMe.payload.old_faction_id
                        + "\n [][] zoneID: " + passMe.zoneId + " REAL ACTION: " + passMe.facilityAction + " on server: " + passMe.payload.World_id);

                }

                if (e.Payload.old_faction_id == e.Payload.new_faction_id && e.Payload.new_faction_id != 0)
                {
                    //it's a defense payload
                    passMe = new VisualDefensePayload()
                    {
                        payload = e.Payload,
                        name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                        zoneId = e.Payload.Zone_id,
                        continent = fR.GetContinentName(e.Payload.Zone_id)
                    };
                    Console.WriteLine("[][] DEBUG [][] DEFEND passMe param:  " + passMe.continent + " / " + passMe.name + " / " + passMe.payload.new_faction_id + " from " + passMe.payload.old_faction_id
                        + "\n [][] zoneID: " + passMe.zoneId + " REAL ACTION: " + passMe.facilityAction + " on server: " + passMe.payload.World_id);

                }

                Console.WriteLine("[][] DEBUG [][] If loops passed.\n");
                //if (passMe.zoneName != null && (passMe.zoneName != "UNKNOWN FACILITY*") && passMe.zoneName.Contains("Koltyr") == false)


                if (passMe.continent != null && (passMe.name != "UNKNOWN FACILITY*") && passMe.name.Contains("Koltyr") == false)
                {
                    if (passMe.continent != "UNKNOWN CONTINENT (zoneId)*")
                    {
                        if (((subscribedMessages.Count >= 1) && subscribedMessages[subscribedMessages.Count - 1].payload.Timestamp != passMe.payload.Timestamp)
                            || subscribedMessages.Count == 0)
                        {
                            subscribedMessages.Add(passMe);
                            Console.WriteLine("\n[][] DEBUG [][]DISPLAY SHOULD UPDATE WHEN THIS IS PRINTED\n");
                        }
                        else { Console.WriteLine("[][] DEBUG [][] SKIPPED ITEM"); }
                    }
                }
            }
        }




        private string ResolveFactionId(int faction_id)
        {
            //if (faction_id == 1) return "Vanu Sovereignity";
            //if (faction_id == 2) return "Terran Republic";
            //if (faction_id == 3) return "New Conglomerate";

            if (faction_id == 1) return "VS";
            if (faction_id == 2) return "NC";
            if (faction_id == 3) return "TR";
            else return "UNKNOWN FACTIONID!*";
        }

        private async void PlanetsideService_MetagameEventChangedAsync(object sender, Events.World.MetagameEventEventArgs e)
        {
            if (e != null)
            {
                VisualPayload passMe;
                if (e.Payload.metagame_event_id != null)
                {
                    Console.WriteLine("\n[][] DEBUG [][] EVENT PAYLOAD ON " + e.Payload.World_id +  "   \n");
                    var notifServ = Xamarin.Forms.DependencyService.Resolve<INotificationService>();
                    if (e.Payload.metagame_event_id == "159" || e.Payload.metagame_event_id == "160" || e.Payload.metagame_event_id == "162" || e.Payload.metagame_event_id == "163")
                        passMe = new VisualNonmetaPayload()
                        {
                            payload = e.Payload,
                            name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                            zoneId = e.Payload.Zone_id,
                            continent = fR.GetContinentName(e.Payload.Zone_id),
                            event_id = int.Parse(e.Payload.metagame_event_id),
                            eventStatus = e.Payload.metagame_event_state_name,
                            nc = int.Parse(e.Payload.faction_nc),
                            tr = int.Parse(e.Payload.faction_tr),
                            vs = int.Parse(e.Payload.faction_vs)
                        };
                    else
                    {
                        passMe = new VisualEventPayload()
                        {
                            payload = e.Payload,
                            name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                            zoneId = e.Payload.Zone_id,
                            continent = fR.GetContinentName(e.Payload.Zone_id),
                            event_id = int.Parse(e.Payload.metagame_event_id),
                            eventStatus = e.Payload.metagame_event_state_name,
                            nc = double.Parse(e.Payload.faction_nc),
                            tr = double.Parse(e.Payload.faction_tr),
                            vs = double.Parse(e.Payload.faction_vs)
                        };
                    }
                    Console.WriteLine("[][] DEBUG [][] EVENT passMe param:  " + passMe.continent + " / " + passMe.name + " / " + passMe.payload.new_faction_id + " from " + passMe.payload.old_faction_id
                        + "\n [][] zoneID: " + passMe.zoneId + " REAL ACTION: " + passMe.facilityAction + " on server: " + passMe.payload.World_id);
                    var d = MatchEvents(e);
                    
                    if (d != null 
                        //&& e.Payload.World_id == worldId  /*uncomment to be server specific*/
                        && passMe.payload.new_faction_id != 0)
                    {
                        passMe.eventName = d.event_name;
                        string message = ($"{d.event_name} on {d.continent} {e.Payload.metagame_event_state_name} at " +
                            epoch.AddSeconds(e.Payload.Timestamp).ToLocalTime().ToShortTimeString());

                        await NotifyUserAsync("Planetside Alert", message);
                        subscribedMessages.Add(passMe);
                    }
                }
            }
        }

        private Events.World.Event MatchEvents(Events.World.MetagameEventEventArgs check)
        {
            Events.World.Event localCheck = null;
            for (int i = 0; i < _events.Length; i++)
            {
                if (check.Payload.metagame_event_id == _events[i].event_id.ToString())
                {
                    localCheck = _events[i];
                    break;
                }
            }
            if (localCheck != null)
            {
                return localCheck;
            }
            else
            {
                Console.WriteLine("\n\n-----ERROR PARSING EVENT INFORMATION FROM LOCAL DATABASE-------\n\n");
                return null;
            }
        }


        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row    
        }

        private async void startSubscription_Clicked(object sender, EventArgs e)
        //add some sort of failsafe (probably a bool value) 
        {
            if (_IsStartButtonRunning == false)
            {
                _IsStartButtonRunning = true;
                startSubscription.IsEnabled = false;
                //PlanetsideService planetsideService = new PlanetsideService();

                await planetsideService.StartAsync();

            }
            var returnedtask = await fR.GetListAsync();
            _IsStartButtonRunning = false;
            stopLive.IsEnabled = true;
        }
        
        async private void stopLive_Clicked(object sender, EventArgs e)
        {
            await planetsideService.StopAsync();
            //re enable start button?
        }
        
        private void clearList_Clicked(object sender, EventArgs e)
        {
            subscribedMessages.Clear();
        }

        public Task NotifyUserAsync(string title, string message)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return Task.Factory.StartNew(() =>
                {
                    var notifServ = Xamarin.Forms.DependencyService.Resolve<INotificationService>();
                    notifServ.NotifyAsync(title, message);
                });
            }
            else return Task.CompletedTask;
        }
        
        private class Facility
        {
            public string zoneId { get; set; }
            public string continent { get; set; }
        }
    }
}