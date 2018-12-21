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

namespace PsApp
{
    public partial class MainPage : ContentPage
    {
       
        public List<Payloads.FrontpagePayload> AllEvents = new List<Payloads.FrontpagePayload>();
        ObservableCollection<Payloads.FrontpagePayload> subscribedMessages = new ObservableCollection<Payloads.FrontpagePayload>();     
        public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();



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
            if (Preferences.Get("globalWorldId", "40", "theWorld") != null)
            {
                WorldEventListResult a = await GetList();
                PopulateList(a.world_event_list);
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
            subscribedMessages.CollectionChanged += SubscribedMessages_CollectionChanged;
            serverPicker.SelectedIndexChanged += serverPicker_SelectedIndexChanged;
        }
        
        
        private void PopulateList(List<Events.World_Event> n)
        {
            foreach (var i in n)
            {
                //if(i.metagame_event_id == "123" || i.metagame_event_id == "147" || i.metagame_event_id == "124" || i.metagame_event_id == "148" || i.metagame_event_id == "125" || i.metagame_event_id == "149" || i.metagame_event_id == "126" || i.metagame_event_id == "150" || i.metagame_event_id == "127" || i.metagame_event_id == "151" || i.metagame_event_id == "128" || i.metagame_event_id == "152" || i.metagame_event_id == "129" || i.metagame_event_id == "153" || i.metagame_event_id == "130" || i.metagame_event_id == "154" || i.metagame_event_id == "131" || i.metagame_event_id == "155" || i.metagame_event_id == "132" || i.metagame_event_id == "156" || i.metagame_event_id == "133" || i.metagame_event_id == "157" || i.metagame_event_id == "134" || i.metagame_event_id == "158" || i.metagame_event_id == "159" || i.metagame_event_id == "160" || i.metagame_event_id == "162" || i.metagame_event_id == "163" || i.metagame_event_id == "176" || i.metagame_event_id == "186" || i.metagame_event_id == "190" || i.metagame_event_id == "177" || i.metagame_event_id == "187" || i.metagame_event_id == "191" || i.metagame_event_id == "178" || i.metagame_event_id == "188" || i.metagame_event_id == "192" || i.metagame_event_id == "179" || i.metagame_event_id == "189" || i.metagame_event_id == "193")
                if (i.event_type == "MetagameEvent")
                {

                    //fetch cont name from id
                    var anEvent = MatchEvents(i);
                    Payloads.FrontpagePayload passMe = new Payloads.FrontpagePayload()
                    {
                        continent = anEvent.continent,
                        eventName = anEvent.event_name,
                        faction_nc = int.Parse(i.faction_nc),
                        faction_tr = int.Parse(i.faction_tr),
                        faction_vs = int.Parse(i.faction_vs),
                        timestamp =  i.timestamp,
                        world_id = i.world_id,
                        metagame_event_state_name = i.metagame_event_state_name,
                        metagame_event_id = i.metagame_event_id,
                        instance_id = i.instance_id
                        //debug
                        ,
                        event_type = i.event_type

                    };
                    //add the item to the observable collection
                    if (passMe.eventName.Contains("Hossin") ||
                        passMe.eventName.Contains("Indar") ||
                        passMe.eventName.Contains("Amerish") ||
                        passMe.eventName.Contains("Esamir"))
                    {
                        if(((subscribedMessages.Count >= 1) && subscribedMessages[subscribedMessages.Count - 1].timestamp != passMe.timestamp)
                            || subscribedMessages.Count == 0)
                            subscribedMessages.Add(passMe);
                    }
                    recentEvents.ItemsSource = subscribedMessages;
                }
                //make an overloaded version of this that takes the globalWorldId 
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
                Console.WriteLine("\n\n-----ERROR PARSING EVENT INFORMATION FROM LOCAL DATABASE-------\n\n");
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
            await notifServ.NotifyBigAsync("test title", "message message");
        }

        async void serverPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preferences.Set("globalWorldId", serverPicker.SelectedItem.ToString(),"theWorld");
            WorldEventListResult a = await GetList();
            PopulateList(a.world_event_list);
            //RefreshList();
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

    }
    public class World
    {
        public int theId { get; set; }
        public string theName { get; set; }
    }

}