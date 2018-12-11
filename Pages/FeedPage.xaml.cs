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
using System.ComponentModel;

namespace PsApp
{

    //listview that displays object of type Event

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedPage : ContentPage//, INotifyPropertyChanged 
	{
        //define a view for a facility control event
        public View FacilityView(VisualPayload vP)
        {


            View v = new FlexLayout
            {
                Children =
                {

                }
            }; 
            return v;
        }


        private bool _IsStartButtonRunning = false;
        const string serviceID = "PS2mobile2018";

        List<string> socketOutput = new List<string>();
        PlanetsideService planetsideService;
        FacilityResolver fR;

        int worldId;
        public FeedPage (int SelectedWorld)
		{
            this.worldId = SelectedWorld;
            fR= new FacilityResolver();


            InitializeComponent();
            planetsideService = new PlanetsideService(serviceID);
            //planetsideService.MetagameEventChange += PlanetsideService_MetagameEventChanged;
            planetsideService.ConnectionStateChanged += PlanetsideService_ConnectionStateChanged;
            //planetsideService.ContinentLocked += PlanetsideService_ContinentLockChanged;
            //planetsideService.ContinentUnlocked += PlanetsideService_ContinentUnlockChanged;
            planetsideService.FacilityControlChanged += PlanetsideService_FacilityControlChanged;
            consoleOut.ItemsSource = subscribedMessages;
        }

        private void PlanetsideService_ContinentLockChanged(object sender, ContinentLockEventArgs e)
        {

        }
        private void PlanetsideService_ContinentUnlockChanged(object sender, ContinentUnlockEventArgs e)
        {

        }

        

        ObservableCollection<VisualPayload> subscribedMessages = new ObservableCollection<VisualPayload>();
        

        private void PlanetsideService_ConnectionStateChanged(object sender, MessageEventArgs e)
        {
            Console.WriteLine("---------------------EVENTRAISED ON RECEIVING END-----------------------------");
            if(e != null)
            {
                if (e.connectionStatus == "true")
                {
                    statusLabel.Text = "Online";
                    statusLabel.TextColor = Color.Green;
                }

                if (e.connectionStatus == "false")
                {
                    statusLabel.Text = "Offline";
                    statusLabel.TextColor = Color.Red;
                }
            }
        }
        

        private void PlanetsideService_FacilityControlChanged(object sender, FacilityControlChangedEventArgs e)
        {
            if (e != null)
            {


                //foreach(var existingMessage in subscribedMessages)
                //{
                //    if (e.Payload.Timestamp == e.Payload.Timestamp)
                //    {
                //        // don't add it!!
                //        return;
                //    }
                //}
                //convert args from faclity control to the generic payload
                VisualPayload passMe = new VisualPayload()
                {
                    payload = e.Payload,
                    name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                    zoneId = e.Payload.Zone_id,
                    continent = fR.GetContinentName(e.Payload.Zone_id)
                    
                };
                //if (passMe.zoneName != null && (passMe.zoneName != "UNKNOWN FACILITY*") && passMe.zoneName.Contains("Koltyr") == false)
                passMe.payload = e.Payload;

                if (e.Payload.duration_held != 0)
                {

                    if (passMe.continent != null && (passMe.name != "UNKNOWN FACILITY*") && passMe.name.Contains("Koltyr") == false)
                    {
                        if (passMe.continent != "UNKNOWN CONTINENT (zoneId)*")
                        {
                            //string add = $"{newFact} has captured {passMe.name} from {oldFact} \n " +
                            //            $"on {passMe.continent} at {FromUnixTime(e.Payload.Timestamp).ToLocalTime().ToLongTimeString()}";



                            consoleOut.


                            subscribedMessages.Add(passMe);
                            Console.WriteLine("YEP ITS A FACILITY PAYLOAD   \n" + e.Payload.ToString());
                        }

                    }
                }


                ////assign faction strings
                //string oldFact, newFact;

                //oldFact = ResolveFactionId(e.Payload.old_faction_id);
                //newFact = ResolveFactionId(e.Payload.new_faction_id);

                //// change this string to something more meaningful
                ////subscribedMessages.Add($"Facility Control Changed : {e.Payload}");

                //Facility myPlace = new Facility()
                //{
                //    name = fR.FetchFacilityNameFromMasterList(e.Payload.facility_id),
                //    zoneId = e.Payload.Zone_id,
                //    continent = fR.GetContinentName(e.Payload.Zone_id)
                //};
                //string action = string.Empty;
                ////if (e.Payload.duration_held != 0) action = "captured";
                ////if (e.Payload.duration_held == 0) action = "defend"; // need to look at this again later
                
                //if(e.Payload.duration_held != 0)
                //    {
                    
                //        if (myPlace != null && (myPlace.name != "UNKNOWN FACILITY*") && myPlace.name.Contains("Koltyr") == false)
                //        {
                //            if (myPlace.continent != "UNKNOWN CONTINENT (zoneId)*")
                //            {
                //            //Console.WriteLine($"\n\n{myPlace.ToString()} \nend string \n)");
                //            string add = $"{newFact} has captured {myPlace.name} from {oldFact} \n " +
                //                        $"on {myPlace.continent} at {FromUnixTime(e.Payload.Timestamp).ToLocalTime().ToLongTimeString()}";

                //            if ((myPreviousPlace != myPlace)
                //                && subscribedMessages.ElementAt(subscribedMessages.Count).ToString() != add /* && (prevInstance.Payload.Timestamp != e.Payload.Timestamp)*/) //dont add duplicates
                //                {
                //                             subscribedMessages.Add(add);
                //                    Console.WriteLine("YEP ITS A FACILITY PAYLOAD   \n" + e.Payload.ToString());

                //                }
                //            if (subscribedMessages.Count != 0) myPreviousPlace = myPlace;
                //                //if (subscribedMessages.Count != 0) prevInstance.Payload = e.Payload;


                //            //use the facility resolver class to get a better visual output
                //            //subscribedMessages.Add($"Facility Control Changed : {e.Payload.facility_id}");
                //            }

                //        }
                //    }
            }
        }


        //killfeed class defined in other file
        //methods: GetIdByName


        private string ResolveFactionId(int faction_id)
        {
            //if (faction_id == 1) return "Vanu Sovereignity";
            //if (faction_id == 2) return "Terran Republic";
            //if (faction_id == 3) return "New Conglomerate";

            if (faction_id == 1) return "VS";
            if (faction_id == 2) return "TR";
            if (faction_id == 3) return "NC";
            else return "UNKNOWN FACTIONID!*";
        }

        private void PlanetsideService_MetagameEventChanged(object sender, Events.World.MetagameEventEventArgs e)
        {
            if (e != null)
            {

                //just add e to the collection(see the other code)
                VisualPayload vP = new VisualPayload()
                {
                    payload = e.Payload,
                    event_id = int.Parse(e.Payload.metagame_event_id),
                    eventStatus = e.Payload.metagame_event_state_name,
                    nc = double.Parse(e.Payload.faction_nc),
                    tr = double.Parse(e.Payload.faction_tr),
                    vs = double.Parse(e.Payload.faction_vs)
                };

                //if warpgate event, reformat syntax
                //if(metagameEvent.eventName=="Warpgates"

                //if (e.Payload.Event_name == "Warpgates")
                //{
                //    if (metagameEvent.eventStatus == "started") metagameEvent.eventStatus = "stabilizing";
                //    if (metagameEvent.eventStatus == "ended") metagameEvent.eventStatus = "stabilized";

                //    subscribedMessages.Add($"{metagameEvent.eventName} {e.Payload.metagame_event_state_name} on {metagameEvent.eventCont} \n {FromUnixTime(e.Payload.Timestamp).ToLocalTime().ToLongTimeString()}");
                //} 

                //else if (e.Payload.metagame_event_state_name == "started" || ((e.Payload.metagame_event_state_name == "ended") && (e.Payload.metagame_event_state_name == "Warpgates")))
                //{

                //    subscribedMessages.Add($"Event {metagameEvent.eventStatus}: {metagameEvent.eventName} on {metagameEvent.eventCont}");
                //}


                //else if (metagameEvent.eventStatus == "ended")
                //{
                //    subscribedMessages.Add($"Event {metagameEvent.eventStatus}: {metagameEvent.eventName} on {metagameEvent.eventCont}\n" +

                //    $"VS:{(int)metagameEvent.vs} TR:{(int)metagameEvent.tr} NC:{(int)metagameEvent.nc}");
                //}


                

                subscribedMessages.Add(vP);
            }
                        // change this string to something more meaningful
            //subscribedMessages.Add($"Metagame event : {e.Payload} \n {e.Payload.faction_nc}");
            //use the facility resolver class to get a better visual output
            //facilityControlMessages.Add($"Facility Control Changed : {e.Payload.facility_id}");
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            //add something to account for whatever timezone is in use 
            return epoch.AddSeconds(unixTime);
        }
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


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
                get; set;
                //    get
                //    {
                //        if (zoneId == "2") return "Indar";
                //        if (zoneId == "4") return "Hossin";
                //        if (zoneId == "6") return "Amerish";
                //        if (zoneId == "8") return "Esamir";
                //        else return "UNKOWN CONTINENT (zoneId)*";
                //    }
            }

            

        }


        private class MetagameEvent
        {
            public int event_id { get; set; }
            public string eventCont { get; private set; }
            public string eventStatus{ get; set; }
            public string eventName
            {
                //separate continent un/lockings when i add ContinentLockEventHandlers
                get
                {
                    if (event_id == 9)
                    { eventCont = "Amerish"; return "Power Rush"; }

                    if (event_id == 14)
                    { eventCont = "Esamir"; return "Power Rush"; }

                    if (event_id == 16)
                    { eventCont = "Hossin"; return "Power Rush"; }

                    if (event_id == 151)
                    { eventCont = "Esamir"; return "Continent Locked"; }

                    if (event_id == 152)
                    { eventCont = "Esamir"; return "Continent Alert"; }

                    if (event_id == 158)
                    { eventCont = "Amerish"; return "Continent Alert"; }

                    if (event_id == 159)
                    { eventCont = "Amerish"; return "Warpgate"; }

                    if (event_id == 160)
                    { eventCont = "Esamir"; return "Warpgate"; }

                    if (event_id == 161)
                    { eventCont = "Indar"; return "Warpgate"; }

                    if (event_id == 162)
                    { eventCont = "Hossin"; return "Warpgate"; }



                    if (event_id == 167)
                    { eventCont = "Indar?"; return "Aerial Anomaly(?)"; }

                    if (event_id == 172)
                    { eventCont = "Amerish"; return "Aerial Anomaly"; }

                    if (event_id == 173)
                    { eventCont = "Esamir"; return "Aerial Anomaly"; }
                    
                    if (event_id == 174)
                    { eventCont = "Hossin"; return "Aerial Anomaly"; } 

                    
                    if (event_id == 180)
                    { eventCont = "Indar"; return "Gaining Ground"; }

                    if (event_id == 182)
                    { eventCont = "Amerish"; return "Gaining Ground"; }

                    if (event_id == 183)
                    { eventCont = "Hossin"; return "Gaining Ground"; }
                    return $"UNKNOWN EVENT ID: {event_id}*";
                }

            }
            public double nc { get; set; }
            public double tr { get; set; }
            public double vs { get; set; } 

        }
        

    }

}