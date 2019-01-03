using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PsApp
{
    class BGDownloadTask
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
                    metagame_event_id = item.metagame_event_id,
                    timestamp = item.timestamp,
                    world_id_int = int.Parse(item.world_id)
                });
            }

            return compactEventList;
        }
    }
}
// need to convert to this format for better performance at some point

//namespace FormsBackgrounding
//{
//    public class TaskCounter
//    {
//        public async Task RunCounter(CancellationToken token)
//        {
//            await Task.Run(async () => {

//                for (long i = 0; i < long.MaxValue; i++)
//                {
//                    token.ThrowIfCancellationRequested();

//                    await Task.Delay(250);
//                    var message = new TickedMessage
//                    {
//                        Message = i.ToString()
//                    };

//                    Device.BeginInvokeOnMainThread(() => {
//                        MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
//                    });
//                }
//            }, token);
//        }
//    }
//}