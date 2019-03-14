using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using PsApp.Messages;
namespace PsApp.Droid.Services
{
    [Service]
    public class EventCheckerService : Service
    {
        //const int NOTIFICATION_ID = 9000;
        
        //method to check if any of the events are desirable and if so, notify the user
        public CompactWorldEvent CheckForWantedEvents(List<CompactWorldEvent> results)
        {
            int index = -1;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].eventName.Contains("Amerish") ||
                           results[i].eventName.Contains("Esamir") ||
                               results[i].eventName.Contains("Indar") ||
                                   results[i].eventName.Contains("Hossin"))
                {
                    //call the method
                    index = i;
                    break;
                }
            }
            //notify the user 
            if (index == -1)
            {
                //nothing of interest so don't do anything.
                return new CompactWorldEvent() { event_type = "void" };
            }
            return results[index];            
        }
        EventListDownloader eventDownloader = new EventListDownloader();

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            //later add an if statement to ensure this DOESN'T START unless the user has the setting turned on via preferences 
            var notifService = DependencyService.Resolve<INotificationService>();
            //notifService.NotifyAsync("Test", "Method call in EventCheckerService is successful",3);

            //start a broadcast service so we can be ready to call this more than once 

            //when we receive the alarm to call this event: 
            CheckForNewEvents();
            return StartCommandResult.Sticky;
        }

        private void CheckForNewEvents()
        {
            Task.Run(async () =>
            {
                var  downloadedEvents = await eventDownloader.GetMetagameEventsAsync();
                var notifService = DependencyService.Resolve<INotificationService>();
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                //this should not be on the start command unless we're implementing the time to wait BETWEEN the calls.
                var notifiableEvent = CheckForWantedEvents(downloadedEvents);
                //download the events to a json 

                if (notifiableEvent.event_type != "void")
                {
                    //notify the user 
                    var message = new DownloadMessage
                    {
                         NotifyingEvent = notifiableEvent
                    };
                    //MessagingCenter.Send(message, "Download");
                    string theTime = epoch.AddSeconds((double)notifiableEvent.timestamp).ToLocalTime().ToLongTimeString();
                    //await notifService.NotifyAsync("Alert started!", $"{notifiableEvent.eventName} on IMPLEMENT CONTINENT NAME at {theTime}",2);
                    await notifService.NotifyBigAsync("Alert started!", $"{notifiableEvent.eventName}", notifiableEvent, theTime);
                }
            });
        }
    }

    class EventListDownloader
    {
        /// <summary>
        /// Download up to 1000 latest events of type METAGAME from the Historic Events collection 
        /// </summary>
        /// <returns>Task-wrapped List of type CompactWorldEvent</returns>
        public async Task<List<CompactWorldEvent>> GetMetagameEventsAsync()
        {
            EventsHelper eventsHelper = new EventsHelper();
            string pref = Preferences.Get("globalWorldId", "100", "theWorld");
            int time = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds) - 28800; //28800 is last 8 hours 
            string json;
            using (var client = new WebClient())
            {
                string uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?world_id={pref}&after={time}&type=METAGAME&c:limit=200";
                if (pref == "100") //if we're debugging
                    uri = $"https://census.daybreakgames.com/s:trashpanda/get/ps2:v2/world_event/?after={time}&type=METAGAME&c:limit=200";

                json = await client.DownloadStringTaskAsync(uri);
            }
            Events.WorldEventListResult recentList = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.WorldEventListResult>(json);


            var compactEventList = new List<CompactWorldEvent>();
            foreach (Events.World_Event item in recentList.world_event_list)
            {
                compactEventList.Add(new CompactWorldEvent()
                {
                    eventName = eventsHelper.MatchEvents(item).event_name,
                    event_type = item.event_type,
                    metagame_event_id = item.metagame_event_id,
                    timestamp = item.timestamp,
                    world_id_int = int.Parse(item.world_id)
                });
            }

            return compactEventList;
        }
    }
}