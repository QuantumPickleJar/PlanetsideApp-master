using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using PsApp.Events;
using Xamarin.Essentials;
using Xamarin.Forms;
using PsApp.Droid.Services;
using PsApp.Messages;
namespace PsApp.Droid 
{
    [Activity(Label = "PsApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        //private bool _FeedPage
        private static Context _context;
        private double _timeStart;
        private double _timeOfPause;
        public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();

        /// <summary>
        /// "Primes" the service to run by telling the MessagingCenter that we're subscribing to a message.
        /// </summary>
        void WireDownloadingTask()
        {

            var intent = new Intent(this, type: typeof(EventCheckerService));
            StartService(intent);

            //subscribe to the event so we know what to do 
            //MessagingCenter.Subscribe<DownloadMessage>(this, "Download", message =>
            //{
            //    var intent = new Intent(this, type: typeof(EventCheckerService));
            //    StartService(intent);
            //});
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            WireDownloadingTask();
            Preferences.Set("globalWorldId", "17","theWorld");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            _timeStart = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;

            base.OnCreate(savedInstanceState);
            
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.DependencyService.Register<INotificationService, NotificationServiceForAndroid>();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            NotificationServiceForAndroid.Initialize(this);

            LoadApplication(new App());
            _IsSleeping = false;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnPause()
        {
            _IsSleeping = true;
            _timeOfPause = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            Console.Write("\n\nPaused!\n\n");
            CheckMissedEventsAsync();
            base.OnPause();
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



        protected override void OnResume()
        {
            _IsSleeping = false;
            base.OnResume();
            //add code that will add missed items into the live page but 
            // ONLY IF THE LIVE PAGE WAS THE LAST ACTIVE PAGE WHEN WE PAUSED THE APP
        }


        private async void StartTimer()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(60000); // 60 seconds
            await TimerAsync(1000, cts.Token);
        }
        

        //task factory maybe?
        private async Task TimerAsync(int interval, CancellationToken token)
        {
            try
            {
                while (token.IsCancellationRequested)
                { 
                    await CheckMissedEventsAsync();
                    await Task.Delay(interval, token);
                }
                await CheckMissedEventsAsync();
                await Task.Delay(interval, token);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("---------------------- ! ! ! ! TASK CANCELED EXCPETION CAUGHT (TimerAsync)! ! ! ! --------------------------");
                return;
            }
        }

        private bool _IsSleeping;
        public async Task CheckMissedEventsAsync()
        {
            while (_IsSleeping==false)
            {
                string json;
                using (var client = new WebClient())
                {
                    string uri = $"https://census.daybreakgames.com/get/ps2:v2/world_event/?world_id=17&after={(int)_timeOfPause}&c:limit=500";
                    //if(globalWorldId!=null) uri = $"https://census.daybreakgames.com/get/ps2:v2/world_event/?world_id={globalWorldId}&after={(int)_timeOfPause}&c:limit=500";
                    json = await client.DownloadStringTaskAsync(uri);
                }
                Events.WorldEventListResult missedEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.WorldEventListResult>(json);
                // check if any of the downloaded event types are continent events
                //foreach (Events.World_Event item in missedEvents.world_event_list)
                for (int i = 0; i < missedEvents.world_event_list.Count; i++)
                {
                    if (missedEvents.world_event_list[i].event_type == "MetagameEvent")
                    {
                        var d = MatchEvents(missedEvents.world_event_list[i]);
                        if (d != null)
                        {
                            //make a notification to the user
                            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            var notifServ = Xamarin.Forms.DependencyService.Resolve<INotificationService>();
                            double stamp = (double)(missedEvents.world_event_list[i].timestamp);
                            notifServ.NotifyOld("Planetside Event", $"{d.event_name} on {d.continent} {missedEvents.world_event_list[i].metagame_event_state_name} at " +
                                epoch.AddSeconds(stamp).ToLocalTime().ToShortTimeString());
                        }
                    }
                }
            }
        }


        protected override void OnDestroy()
        {
            //_timeEnd = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            
            //AlertDialog.Builder builder = new AlertDialog.Builder(_context);
            //builder.SetTitle("App killed");
            //builder.SetMessage("Quant has been killed, either by the System or the user.  \n" +
            //    "If you're seeing this and you didn't just exit the app intentionally, " +
            //    "please send me either a screenshot or the following information:\n"
            //    + _timeStart + " to " + _timeEnd);
            //builder.Create();
            base.OnDestroy();
        }
    }
}