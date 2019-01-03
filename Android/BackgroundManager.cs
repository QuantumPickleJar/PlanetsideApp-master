using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Android.App.Job;
using Android.App;
using System.Threading.Tasks;
using Android.Widget;
using System.Net;
using System.Threading;

namespace PsApp
{
    [Service(Name = "com.PsApp.refresher", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class BackgroundManager : JobService
    {
        public override bool OnStartJob(JobParameters @params)
        {


            double unix = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            Console.WriteLine("-------------------------Job Started-------------------------");

            //Thread thread;
            //thread = new Thread(DoBackgroundWork(@params));
            //thread.Start();


            //make sure to do this on A DIFFERENT THREAD 
            Task.Run(async () =>
            {
               

                //create a timespan 


            //while app is not active 

                //count down from the Timespan of 30seconds

                //if(timeSpan.timeRemaining == 0)
                string json;

                using (var client = new WebClient())
                {
                    string uri = $"https://census.daybreakgames.com/get/ps2:v2/world_event/?world_id=17&after={unix}&c:limit=50";

                    json = await client.DownloadStringTaskAsync(uri);
                }

                Events.WorldEventListResult missedEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<Events.WorldEventListResult>(json);

                // check if any of the downloaded event types are continent events
                for (int i = 0; i < missedEvents.world_event_list.Count; i++)
                {
                    if (missedEvents.world_event_list[i].event_type == "MetagameEvent")
                    {
                        //make a notification to the user
                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var notifServ = Xamarin.Forms.DependencyService.Resolve<INotificationService>();
                        //double stamp = Double.Parse(missedEvents.world_event_list[i].timestamp);
                        double stamp = (double)missedEvents.world_event_list[i].timestamp;
                        await notifServ.NotifyAsync("Planetside Alert", $"{missedEvents.world_event_list[i].metagame_event_id} started at " +
                            epoch.AddSeconds(stamp).ToLocalTime().ToShortTimeString());
                    }
                }
                JobFinished(@params, false);
            });


            //return true becasue async work
            return true;
        }

        //async private void DoBackgroundWork(JobParameters @params)
        //{
        //    while(
        //}

        public override bool OnStopJob(JobParameters @params)
        {
            //don't reschedule since we're done/cancelled
            //bool _IsAppAsleep = //is the app asleep?
            return true;
        }


    }
}
