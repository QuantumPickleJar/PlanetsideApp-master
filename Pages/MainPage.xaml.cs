using PsApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using PsApp.Events;
using System.Threading;

namespace PsApp
{
    public partial class MainPage : ContentPage
    {

        public List<Payloads.FrontpagePayload> AllEvents = new List<Payloads.FrontpagePayload>();
        ObservableCollection<Payloads.FrontpagePayload> subscribedMessages = new ObservableCollection<Payloads.FrontpagePayload>();
        public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();
        public SynchronizationContext SynchronizationContext { get; }



        public World[] servers = {
                                     new World{theId = 1, theName= "Connery"},
                                     new World{theId = 10, theName= "Miller"},
                                     new World{theId = 13, theName= "Cobalt"},
                                     new World{theId = 17, theName= "Emerald"},
                                     new World{theId = 19, theName= "Jaeger"},
                                     new World{theId = 25, theName= "Briggs"},
                                     new World{theId = 40, theName= "SolTech"},
                                     new World{theId = 100, theName= "All(Debug)" },
                                 };


        protected override async void OnAppearing()
        {
            try
            {
                if (Preferences.Get("globalWorldId", "40", "theWorld") != null)
                {
                    //var debug = new Payloads.DebugPayload() { message = "Loading..." };
                    //subscribedMessages.Add(debug);
                    //WorldEventListResult a = await GetList();
                    AnimatedRefreshList();
                }
            }
            catch (WebException e)
            {
                string s = e.Message;
                subscribedMessages.Add(new Payloads.DebugPayload() { message = "Network error.  Are you connected to the internet?" });
                subscribedMessages.Add(new Payloads.DebugPayload() { message = s });

            }
        }



        public MainPage()
        {
            InitializeComponent();
            //navCharacters.IsEnabled = false;
            //navCont.IsEnabled = false;
            //navFisu.IsEnabled = false;
            serverPicker.ItemsSource = servers;
            recentEvents.ItemsSource = subscribedMessages;
            //subscribedMessages.CollectionChanged += SubscribedMessages_CollectionChanged;
            serverPicker.SelectedIndexChanged += serverPicker_SelectedIndexChanged;
            IsLoadingChanged += MainPage_IsRunningChanged;
        }

        private void MainPage_IsRunningChanged(object sender, EventArgs e)
        {
            if (IsLoading == true)
            {
                recentEvents.IsEnabled = false;
                recentEvents.IsVisible = false;
                feedLoader.IsRunning = true;
            }
            if (IsLoading == false)
            {
                recentEvents.IsEnabled = true;
                recentEvents.IsVisible = true;
                feedLoader.IsRunning = false;
            }
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row    
        }

        private Events.World.Event MatchEvents(World_Event world_Event)
        {
            Events.World.Event localCheck = null;
            for (int i = 0; i < _events.Length; i++)
            {
                if (world_Event.metagame_event_id == _events[i].event_id.ToString())
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
                //Console.WriteLine("\n\n-----ERROR PARSING EVENT INFORMATION FROM LOCAL DATABASE-------\n\n");
                return null;
            }
        }




        private void SubscribedMessages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            //if (e != null && subscribedMessages.Count >= 1)
            //{
            //    recentEvents.ScrollTo(subscribedMessages[subscribedMessages.Count - 1], Xamarin.Forms.ScrollToPosition.End, true);
            //}
        }

        async void navCharacters_Clicked(object sender, EventArgs e)
        {
            //if (selectedWorld != 0)
            {
                CharacterSearchPage charSearch = new CharacterSearchPage("PS2mobile2018query");
                await Navigation.PushAsync(charSearch);
            }
        }

        async void navLiveEvent_Clicked(object sender, EventArgs e)
        {
            FeedPageSelect feedPageSelect = new FeedPageSelect();
            await Navigation.PushAsync(feedPageSelect);
        }

        async void navSettingsPage_Clicked(object sender, EventArgs e)
        {
            //SettingsPage settingsPage = new SettingsPage();
            //await Navigation.PushAsync(settingsPage);
        }
        async void navSearchPage_Clicked(object sender, EventArgs e)
        {
            //SettingsPage settingsPage = new SettingsPage();
            //await Navigation.PushAsync(settingsPage);
            var notifServ = DependencyService.Resolve<INotificationService>();
            await Task.Run(() => notifServ.NotifyBigAsync("test title", "message message"));
        }

        private void PopulateList(List<Events.World_Event> n)
        {
            foreach (var i in n)
            {
                Payloads.FrontpagePayload passMe = new Payloads.FrontpagePayload();
                var anEvent = MatchEvents(i);
                if (anEvent == null) return;
                if (i.event_type == "MetagameEvent")
                {

                    if((anEvent.event_name.Contains("Aerial") || anEvent.event_name.Contains("Power") || anEvent.event_name.Contains("Dome")) && i.metagame_event_state_name=="started")
                    {
                        passMe = new Payloads.FrontpageNonmetaPayload()
                        {
                            continent = anEvent.continent,
                            eventName = anEvent.event_name,
                            timestamp = i.timestamp,
                            world_id = i.world_id,
                            world_id_int = int.Parse(i.world_id),
                            metagame_event_state_name = i.metagame_event_state_name,
                            metagame_event_id = i.metagame_event_id,
                            instance_id = i.instance_id,
                            event_type = i.event_type
                        };
                        
                    }

                    else if ((anEvent.event_name.Contains("Gaining") && i.metagame_event_state_name == "started"))
                    {
                        passMe = new Payloads.FrontpageMetaPayload()
                        {                                                                                    /*passMe.continent = anEvent.continent;                            */
                            continent = anEvent.continent,                                                   /*passMe.eventName = anEvent.event_name;                           */
                            eventName = anEvent.event_name,                                                  /*passMe.timestamp = i.timestamp;                                  */
                            timestamp = i.timestamp,                                                         /*passMe.world_id = i.world_id;                                    */
                            world_id = i.world_id,
                            world_id_int = int.Parse(i.world_id),                                            /*passMe.metagame_event_state_name = i.metagame_event_state_name;  */
                            metagame_event_state_name = i.metagame_event_state_name,                         /*passMe.metagame_event_id = i.metagame_event_id;                  */
                            metagame_event_id = i.metagame_event_id,                                         /*passMe.instance_id = i.instance_id;*/
                            instance_id = i.instance_id,                                                         /*passMe.event_type = i.event_type;                                */
                            event_type = i.event_type
                        };

                    }

                    else if ((anEvent.event_name.Contains("Gaining") || anEvent.event_name.Contains("Power") || anEvent.event_name.Contains("Aerial") || anEvent.event_name.Contains("Dome")) 
                        && i.metagame_event_state_name == "ended")
                    {
                        passMe = new Payloads.FrontpageScoredPayload()
                        {
                            continent = anEvent.continent,                                           /*passMe.continent = anEvent.continent;                            */
                            eventName = anEvent.event_name,                                          /*passMe.eventName = anEvent.event_name;                           */
                            faction_nc = int.Parse(i.faction_nc),                                    /*passMe.timestamp = i.timestamp;                                  */
                            faction_tr = int.Parse(i.faction_tr),                                    /*passMe.world_id = i.world_id;                                    */
                            faction_vs = int.Parse(i.faction_vs),                                    /*passMe.metagame_event_state_name = i.metagame_event_state_name;  */
                            timestamp = i.timestamp,                                                 /*passMe.metagame_event_id = i.metagame_event_id;                  */
                            world_id = i.world_id,                                                   /*passMe.instance_id = i.instance_id;*/
                            metagame_event_state_name = i.metagame_event_state_name,                     /*passMe.event_type = i.event_type;                                */
                            metagame_event_id = i.metagame_event_id,
                            instance_id = i.instance_id,
                            world_id_int = int.Parse(i.world_id),
                            event_type = i.event_type
                        };
                    }
                    else if (anEvent.event_name.Contains("Warpgate") || anEvent.event_name.Contains(anEvent.continent) && passMe.instance_id == null )
                    { 
                        passMe = new Payloads.FrontpageContPayload();
                        try
                        {
                            passMe.world_id_int = int.Parse(i.world_id);
                            passMe.continent = anEvent.continent;                                         //{
                            passMe.eventName = anEvent.event_name;                                         //    continent = anEvent.continent,
                            passMe.timestamp = i.timestamp;                                                //    eventName = anEvent.event_name,
                            passMe.world_id = i.world_id;                                                  //    timestamp = i.timestamp,
                            passMe.metagame_event_state_name = i.metagame_event_state_name;                //    world_id = i.world_id,
                            passMe.metagame_event_id = i.metagame_event_id;                                //    metagame_event_state_name = i.metagame_event_state_name,
                            passMe.instance_id = i.instance_id;                                            //    metagame_event_id = i.metagame_event_id,
                            passMe.event_type = i.event_type;                                               //    instance_id = i.instance_id,
                                                                                                            //    event_type = i.event_type                                                    
                                                                                                            //};
                        }
                        catch (NullReferenceException e)
                        {
                            subscribedMessages.Add(new Payloads.DebugPayload() { message = "Error creating meta_event_payload" });
                            Console.WriteLine("\n [][][][][][]Exception caught in first If-loop[][][][][][]\n" + e.InnerException.TargetSite.ToString() + "\n" + e.Message + "\n [][]END[][][][]][]\n");
                        }
                    }
                    else if (passMe.instance_id == null && (!(anEvent.event_name.Contains(anEvent.continent))) && i.metagame_event_state_name == "ended")
                    {
                        passMe = new Payloads.FrontpageNonmetaPayload();
                        passMe.world_id_int = int.Parse(i.world_id);
                        passMe.continent = anEvent.continent;
                        passMe.eventName = anEvent.event_name;
                        passMe.faction_nc = int.Parse(i.faction_nc);
                        passMe.faction_tr = int.Parse(i.faction_tr);
                        passMe.faction_vs = int.Parse(i.faction_vs);
                        passMe.timestamp = i.timestamp;
                        passMe.world_id = i.world_id;
                        passMe.metagame_event_id = i.metagame_event_id;
                        passMe.metagame_event_state_name = i.metagame_event_state_name;
                    }
                    else
                    {
                       
                            passMe = new Payloads.FrontpageScoredPayload()
                            {
                                world_id_int = int.Parse(i.world_id),
                                continent = anEvent.continent,                            /*passMe.continent = anEvent.continent;                          */ 
                               eventName = anEvent.event_name,                           /*passMe.eventName = anEvent.event_name;                         */ 
                               faction_nc = int.Parse(i.faction_nc),                     /*passMe.faction_nc = (int)double.Parse(i.faction_nc);           */         
                               faction_tr = int.Parse(i.faction_tr),                     /*passMe.faction_tr = (int)double.Parse(i.faction_tr);           */         
                               faction_vs = int.Parse(i.faction_vs),                     /*passMe.faction_vs = (int)double.Parse(i.faction_vs);           */         
                               timestamp = i.timestamp,                                  /*passMe.timestamp = i.timestamp;                                */ 
                               world_id = i.world_id,                                    /*passMe.world_id = i.world_id;                                  */ 
                               metagame_event_state_name = i.metagame_event_state_name,  /*passMe.metagame_event_id = i.metagame_event_id;                */ 
                               metagame_event_id = i.metagame_event_id,                  /*passMe.metagame_event_state_name = i.metagame_event_state_name;*/ 
                               instance_id = i.instance_id,                              /*passMe.instance_id = i.instance_id;*/
                               event_type = i.event_type
                            };
                    }

                    //if (passMe.eventName.Contains("Hossin") || passMe.eventName.Contains("Indar") || passMe.eventName.Contains("Amerish") || passMe.eventName.Contains("Esamir") || passMe.world_id == Preferences.Get("globalWorldId", "17", "theWorld")
                        
                    
                        if (((subscribedMessages.Count >= 1) && subscribedMessages[subscribedMessages.Count - 1].timestamp != passMe.timestamp)
                            || subscribedMessages.Count == 0)
                        {
                            bool _isPresent = false;
                            foreach (var item in subscribedMessages)
                            {
                                if (item.instance_id == passMe.instance_id &&
                                    item.metagame_event_state_name == passMe.metagame_event_state_name)
                                    _isPresent = true;
                            }
                            //remove loading messages 

                            if (!_isPresent && (passMe.world_id == Preferences.Get("globalWorldId", "17", "theWorld") || "100" == Preferences.Get("globalWorldId", "17", "theWorld"))) subscribedMessages.Add(passMe);
                            passMe = null;
                        anEvent = null;
                        }
                    
                }
            }
        }




        private Thread thread;

        public async Task StartAsync()
        {
            // create a new thread
            thread = new Thread(RefreshListAsyncTaskless);
            IsLoading = true;
            thread.Start();
            // have the new thread call ListenToWebSocketStuff()

        }




        //fallback method
        async Task RefreshListAsync()
        {

            subscribedMessages.Clear();
            WorldEventListResult a = await GetList();
            PopulateList(FilterList(a.world_event_list));
            //return Task.
        }

        async void RefreshList()
        {
            subscribedMessages.Clear();
            WorldEventListResult a = await GetList();
            PopulateList(FilterList(a.world_event_list));
        }

        async void AnimatedRefreshList()
        {
            subscribedMessages.Clear();
            recentEvents.IsVisible = false;
            feedLoader.IsRunning = true;
            //IsLoading = true;
            await Task.Run(() => RefreshListAsync());
            //IsLoading = false;

            recentEvents.IsVisible = true;
            feedLoader.IsRunning = false;
        }

        async void RefreshListAsyncTaskless()
        {
            subscribedMessages.Clear();
            WorldEventListResult a = await GetList();
            PopulateList(FilterList(a.world_event_list));
        }


        async void serverPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = servers[serverPicker.SelectedIndex].theId;
            Preferences.Set("globalWorldId", i.ToString(), "theWorld");
            //Task.Factory.StartNew(async () => (await RefreshList()));
            AnimatedRefreshList();
            //await Task.Run(() => RefreshListAsyncTaskless());
        }

        private List<World_Event> FilterList(List<World_Event> world_event_list)
        {
            var filteredList = new List<World_Event>();
            foreach (var item in world_event_list.Where(listItem => listItem.event_type == "MetagameEvent"))
            {
                //world_event_list.Remove(item);
                filteredList.Add(item);
            }
            return filteredList;
        }

        /// <summary>
        /// to call:
        ///     WorldEventListResult a = await GetList();
        ///     PopulateList(a.world_event_list);
        /// </summary>
        /// <returns></returns>
        private async Task<WorldEventListResult> GetList()
        {
            PlanetsideService pService = new PlanetsideService("trashpanda");
            Events.WorldEventListResult worldResult = await pService.GetRecentEvents();
            return worldResult;
        }

        private void navCont_Clicked(object sender, EventArgs e)
        {

        }

        private async void navFisuButton_Clicked(object sender, EventArgs e)
        {
            FisuPage fisu = new FisuPage();
            await Navigation.PushAsync(fisu);
        }

        private async void refreshFeed_Clicked(object sender, EventArgs e)
        {
            AnimatedRefreshList();
            //await Task.Run(async () => await RefreshList()); //this throws exception 

            //Task.Factory.StartNew(async()=>await RefreshList());
        }




        public event EventHandler IsLoadingChanged;

        protected virtual void OnRefresh()
        {
            if (IsLoadingChanged != null)
                IsLoadingChanged(this, EventArgs.Empty);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnRefresh();
            }
        }


        public class World
        {
            public int theId { get; set; }
            public string theName { get; set; }
        }

    }
}