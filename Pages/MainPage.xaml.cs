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
        public string localTime;
        public long ts;
        ObservableCollection<Payloads.FrontpagePayload> subscribedMessages = new ObservableCollection<Payloads.FrontpagePayload>();
        //public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();
        public SynchronizationContext SynchronizationContext { get; }
        public PlanetsideService pService = new PlanetsideService("trashpanda");
        EventsHelper manager = new EventsHelper();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.MainPageViewModel();
            serverPicker.ItemsSource = servers;
            recentEvents.ItemsSource = subscribedMessages;
            //subscribedMessages.CollectionChanged += SubscribedMessages_CollectionChanged;
            serverPicker.SelectedIndexChanged += serverPicker_SelectedIndexChanged;
            IsLoadingChanged += MainPage_IsRunningChanged;
        }

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


        //protected override async void OnAppearing()
        protected override void OnAppearing()
        {

            if (ts != 0)
            {
                var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                ts = (long)(epoch - 14400);
                localTime = ts.ToString();
            }

            if (Preferences.Get("globalWorldId", "40", "theWorld") != null)
            {
                ////set the selected index to the preference 
                //int n = int.Parse(Preferences.Get("globalWorldId", "40", "theWorld"));
                //var s = servers.ToList<World>();
                //int index = s.FindIndex(o => o.theId == n);
                //s = null;
                //serverPicker.SelectedItem = servers[index];
                SmoothRefreshList();
            }
        }

        /// <summary>
        /// to call:
        ///     WorldEventListResult a = await GetList();
        ///     PopulateList(a.world_event_list);
        /// </summary>
        /// <returns></returns>
        /// 
        private async Task<WorldEventListResult> GetList()
        {
            Events.WorldEventListResult worldResult = await pService.GetRecentEvents();
            return worldResult;
        }

        private async Task<WorldEventListResult> GetOnlyContinentAlertsAsync()
        {
            Events.WorldEventListResult worldResult = await pService.GetMetagameEVents();
            return worldResult;
        }

        //debug method 
        private WorldEventListResult GetOnlyContinentAlerts()
        {
            Events.WorldEventListResult worldResult = pService.GetMetagameEVents().Result;
            return worldResult;
        }

        async Task RefreshListAsync()
        {
            subscribedMessages.Clear();
            //if (preference to only receive continent alerts)
            WorldEventListResult a = await GetOnlyContinentAlertsAsync();
            AddNonDuplicates(manager.RemoveUselessMessages(a.world_event_list));
        }
        

        async void SmoothRefreshList()
        {
            try
            {
                subscribedMessages.Clear();
                recentEvents.IsVisible = false;
                refreshFeedBtn.IsEnabled = false;
                feedLoader.IsRunning = true;
                //IsLoading = true

                await Task.Run(() => RefreshListAsync());

                //IsLoading = false;
                serverPicker.Unfocus();
                recentEvents.IsVisible = true;
                refreshFeedBtn.IsEnabled = true;
                feedLoader.IsRunning = false;
            }
            catch (WebException e)
            {
                string s = e.Message;
                subscribedMessages.Add(new Payloads.DebugPayload() { message = "Network error.  Are you connected to the internet?" });

            }
        }

        private void MainPage_IsRunningChanged(object sender, EventArgs e)
        {
            if (IsLoading == true)
            {
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
        
        private void AddNonDuplicates(List<Payloads.FrontpagePayload> list)
        {
            foreach (var passMe in list)
            {
                    if (subscribedMessages.Count == 0 || ((subscribedMessages.Count >= 1) && subscribedMessages[subscribedMessages.Count - 1].timestamp != passMe.timestamp))
                {
                    bool _isPresent = false;

                    //if any item in the subscribedMessages exists with the same properties...
                    if (subscribedMessages.Any(message => 
                    (message.timestamp == passMe.timestamp) && message.metagame_event_state_name == passMe.metagame_event_state_name))
                    {
                        //...set this to true so we know not to add it a second time.
                        _isPresent = true;
                    }
                    //remove loading messages 

                    if (!_isPresent && (passMe.world_id == Preferences.Get("globalWorldId", "17", "theWorld") || 
                        "100" == Preferences.Get("globalWorldId", "17", "theWorld")))
                        subscribedMessages.Add(passMe);
                }
            }
        }

        //private Events.World.Event MatchEvents(World_Event world_Event)
        //{
        //    Events.World.Event localCheck = null;
        //    for (int i = 0; i < _events.Length; i++)
        //    {
        //        if (world_Event.metagame_event_id == _events[i].event_id.ToString())
        //        {
        //            localCheck = _events[i];
        //            break;
        //        }
        //    }
        //    if (localCheck != null)
        //    {
        //        return localCheck;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        private void SubscribedMessages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            //if (e != null && subscribedMessages.Count >= 1)
            //{
            //    recentEvents.ScrollTo(subscribedMessages[subscribedMessages.Count - 1], Xamarin.Forms.ScrollToPosition.End, true);
            //}
        }



        ///// <summary>
        ///// DEPRECATED
        ///// <para>Populates the ListView with events from a List<World_Events></para>
        ///// </summary>
        ///// <param name="n">List of World_Event to add </param>
        //private void PopulateList(List<Events.World_Event> n)
        //{

        //    foreach (var i in n)
        //    {

        //        Payloads.FrontpagePayload passMe = new Payloads.FrontpagePayload();
        //        var anEvent = MatchEvents(i);
        //        if (anEvent == null) return;

        //            if (i.event_type == "MetagameEvent")
        //            {

        //                if (anEvent.event_name.Contains("Warpgate"))
        //                {
        //                    //passMe = new Payloads.FrontpageMetaPayload()
        //                    //{
        //                    //    metagame_event_id = i.metagame_event_id,
        //                    //    timestamp = i.timestamp,
        //                    //    eventName = anEvent.event_name,
        //                    //    metagame_event_state_name = i.metagame_event_state_name,
        //                    //    continent = anEvent.continent,
        //                    //    event_type = i.event_type,
        //                    //    world_id_int = int.Parse(i.world_id),
        //                    //    world_id = i.world_id
        //                    //};
        //                    if (i.metagame_event_state_name == "started")
        //                    {
        //                        passMe = new Payloads.FrontpageWarpgateStartPayload()
        //                        {
        //                            metagame_event_id = i.metagame_event_id,
        //                            timestamp = i.timestamp,
        //                            eventName = $"{anEvent.continent} Warpgates",
        //                            continent = anEvent.continent,
        //                            event_type = i.event_type,
        //                            world_id_int = int.Parse(i.world_id),
        //                            world_id = i.world_id
        //                        };
        //                    }
        //                    if (i.metagame_event_state_name == "ended")
        //                    {
        //                        passMe = new Payloads.FrontpageWarpgateEndPayload()
        //                        {
        //                            metagame_event_id = i.metagame_event_id,
        //                            timestamp = i.timestamp,
        //                            eventName = $"{anEvent.continent} Warpgates",
        //                            continent = anEvent.continent,
        //                            event_type = i.event_type,
        //                            world_id_int = int.Parse(i.world_id),
        //                            world_id = i.world_id
        //                        };
        //                    }
        //                }   
        //                //must be an alert
        //                else if (anEvent.event_name.Contains(anEvent.continent))
        //                {
        //                    passMe = new Payloads.FrontpageContPayload()
        //                    {
        //                        metagame_event_id = i.metagame_event_id,
        //                        timestamp = i.timestamp,
        //                        eventName = anEvent.event_name,
        //                        metagame_event_state_name = i.metagame_event_state_name,
        //                        continent = anEvent.continent,
        //                        event_type = i.event_type,
        //                        world_id_int = int.Parse(i.world_id),
        //                        world_id = i.world_id,
        //                        faction_nc = float.Parse(i.faction_nc),
        //                        faction_vs = float.Parse(i.faction_vs),
        //                        faction_tr = float.Parse(i.faction_tr)

        //                    };

        //                }

        //                else if (anEvent.event_name.Contains("Aerial") && i.metagame_event_state_name == "started")
        //                {
        //                    passMe = new Payloads.FrontpageMetaPayload()
        //                    {
        //                        continent = anEvent.continent,                                           /*passMe.continent = anEvent.continent;                            */
        //                        eventName = anEvent.event_name,                                          /*passMe.eventName = anEvent.event_name;                           */
        //                        timestamp = i.timestamp,                                                 /*passMe.metagame_event_id = i.metagame_event_id;                  */
        //                        world_id = i.world_id,                                                   /*passMe.instance_id = i.instance_id;*/
        //                        metagame_event_state_name = i.metagame_event_state_name,                     /*passMe.event_type = i.event_type;                                */
        //                        metagame_event_id = i.metagame_event_id,
        //                        instance_id = i.instance_id,
        //                        world_id_int = int.Parse(i.world_id),
        //                        event_type = i.event_type
        //                    };
        //                }
        //                else if (anEvent.event_name.Contains("Technological")) // && i.metagame_event_state_name == "ended")
        //                {
        //                    passMe = new Payloads.FrontpageExtendedPayload()
        //                    {
        //                        metagame_event_id = i.metagame_event_id,
        //                        timestamp = i.timestamp,
        //                        //eventName = anEvent.event_name,
        //                        eventName = "Tech Adv.",
        //                        metagame_event_state_name = i.metagame_event_state_name,
        //                        continent = anEvent.continent,
        //                        event_type = i.event_type,
        //                        world_id_int = int.Parse(i.world_id),
        //                        world_id = i.world_id,
        //                        faction_nc = (int)float.Parse(i.faction_nc),
        //                        faction_vs = (int)float.Parse(i.faction_vs),
        //                        faction_tr = (int)float.Parse(i.faction_tr)
        //                    };
        //                }
        //                else if (anEvent.event_name.Contains("Power") || anEvent.event_name.Contains("Bio") || anEvent.event_name.Contains("Aerial") || anEvent.event_name.Contains("Gaining")) // && i.metagame_event_state_name == "ended")
        //                {
        //                    passMe = new Payloads.FrontpageScoredPayload()
        //                    {
        //                        metagame_event_id = i.metagame_event_id,
        //                        timestamp = i.timestamp,
        //                        eventName = anEvent.event_name,
        //                        metagame_event_state_name = i.metagame_event_state_name,
        //                        continent = anEvent.continent,
        //                        event_type = i.event_type,
        //                        world_id_int = int.Parse(i.world_id),
        //                        world_id = i.world_id,
        //                        faction_nc = (int)float.Parse(i.faction_nc),
        //                        faction_vs = (int)float.Parse(i.faction_vs),
        //                        faction_tr = (int)float.Parse(i.faction_tr)
        //                    };
        //                }
        //                else
        //                {
        //                try
        //                { 
        //                    passMe = new Payloads.FrontpageScoredPayload()
        //                    {
        //                        world_id_int = int.Parse(i.world_id),
        //                        continent = anEvent.continent,                            /*passMe.continent = anEvent.continent;                          */
        //                        eventName = anEvent.event_name,                           /*passMe.eventName = anEvent.event_name;                         */
        //                        faction_nc = (int)float.Parse(i.faction_nc),                     /*passMe.faction_nc = (int)double.Parse(i.faction_nc);           */
        //                        faction_tr = (int)float.Parse(i.faction_tr),                     /*passMe.faction_tr = (int)double.Parse(i.faction_tr);           */
        //                        faction_vs = (int)float.Parse(i.faction_vs),                     /*passMe.faction_vs = (int)double.Parse(i.faction_vs);           */
        //                        timestamp = i.timestamp,                                  /*passMe.timestamp = i.timestamp;                                */
        //                        world_id = i.world_id,                                    /*passMe.world_id = i.world_id;                                  */
        //                        metagame_event_state_name = i.metagame_event_state_name,  /*passMe.metagame_event_id = i.metagame_event_id;                */
        //                        metagame_event_id = i.metagame_event_id,                  /*passMe.metagame_event_state_name = i.metagame_event_state_name;*/
        //                        instance_id = i.instance_id,                              /*passMe.instance_id = i.instance_id;*/
        //                        event_type = i.event_type
        //                    };
        //                }
        //                catch (Exception e)
        //                {
        //                    passMe = new Payloads.DebugPayload()
        //                    {
        //                        message = "There was an error parsing event information." +
        //                        "\nPlease take a screenshot of this and send it to me on discord.\n" +
        //                        $"TS: {i.timestamp.ToString()}  ID: {i.metagame_event_id}"
        //                    };
        //                }
        //            }
        //        }


        //        if (subscribedMessages.Count == 0 || ((subscribedMessages.Count >= 1) && subscribedMessages[subscribedMessages.Count - 1].timestamp != passMe.timestamp))
        //        {
        //            bool _isPresent = false;
        //            foreach (var item in subscribedMessages)
        //            {
        //                if (item.timestamp == passMe.timestamp &&
        //                    item.metagame_event_state_name == passMe.metagame_event_state_name)
        //                {
        //                    _isPresent = true;
        //                }
        //            }
        //            //remove loading messages 

        //            if (!_isPresent && 
        //                (passMe.world_id == Preferences.Get("globalWorldId", "17", "theWorld") 
        //                || "100" == Preferences.Get("globalWorldId", "17", "theWorld")))
        //                    subscribedMessages.Add(passMe);
        //                    passMe = null;
        //                    anEvent = null;
        //        }

        //    }

        //    if (subscribedMessages.Count == 0)
        //    {
        //        subscribedMessages.Add(new Payloads.DebugPayload() { message = "No events in the past 14400 seconds." });
        //        subscribedMessages.Add(new Payloads.DebugPayload() { message = "Are you on SolTech?" });
        //    }
        //}


            

        private async void RefreshListAsyncTaskless()
        {
            subscribedMessages.Clear();
            //WorldEventListResult a = await GetList();
            //if preference to ONLY receive continent alerts is on
            WorldEventListResult a = await GetOnlyContinentAlertsAsync();
            var b = manager.RemoveUselessMessages(a.world_event_list);
            AddNonDuplicates(b);
        }


        void serverPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = servers[serverPicker.SelectedIndex].theId;
            Preferences.Set("globalWorldId", i.ToString(), "theWorld");
            SmoothRefreshList();   
        }

        private List<World_Event> FilterList(List<World_Event> world_event_list)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            localTime = epoch.AddSeconds(ts).ToLocalTime().ToLongTimeString();
            //spanLatest.SetBinding(Span.TextProperty, ts,c);
            
            var filteredList = new List<World_Event>();
            foreach (var item in world_event_list.Where(listItem => listItem.event_type == "MetagameEvent"))
            {
                //world_event_list.Remove(item);
                filteredList.Add(item);
            }
            return filteredList;
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
            SmoothRefreshList();
            //await Task.Run(async () => await RefreshList()); //this throws exception 

            //Task.Factory.StartNew(async()=>await RefreshList());
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
        async void navSettings_Clicked(object sender, EventArgs e)
        {
            //SettingsPage settingsPage = new SettingsPage();
            //await Navigation.PushAsync(settingsPage);


        }
        async void navOutfit_Clicked(object sender, EventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            await Navigation.PushAsync(settingsPage);
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

        private void myProfBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}