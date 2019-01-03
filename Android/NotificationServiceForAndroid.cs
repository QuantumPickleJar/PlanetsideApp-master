using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace PsApp.Droid
{
    public class NotificationServiceForAndroid : INotificationService
    {

        const string CHANNEL_ID = "MyChannelId";

        private static Context _context;

        public static void Initialize(Context context)
        {
            _context = context;

            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                //UseOldNotifs(context);
                return;
            }

            var channelName = "myChannel";
            var channelDescription = "myChannelDescription";
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public Task NotifyAsync(string title, string message)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                Console.WriteLine("\n---WARNING---: Tried to use new Notification methods on an old device!\n");
                NotifyOldAsync(title, message);
            }
            
            return Task.Factory.StartNew(() =>
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(_context, CHANNEL_ID)
                                                                           .SetContentTitle(title)
                                                                           .SetContentText(message)
                                                                           .SetSmallIcon(Resource.Drawable.planetside2logo)
                                                                           ;

                // Build the notification:
                Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    _context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    builder.SetVisibility(NotificationCompat.VisibilityPublic);
                }

                // Publish the notification:
                const int notificationId = 0;
                notificationManager.Notify(notificationId, notification);

            });
        }

        public Task NotifyAsync(string title, string message, int theId)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                Console.WriteLine("\n---WARNING---: Tried to use new Notification methods on an old device!\n");
                NotifyOldAsync(title, message);
            }

            return Task.Factory.StartNew(() =>
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(_context, CHANNEL_ID)
                                                                           .SetContentTitle(title)
                                                                           .SetContentText(message)
                                                                           .SetSmallIcon(Resource.Drawable.planetside2logo)
                                                                           ;
                // Build the notification:
                Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    _context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    builder.SetVisibility(NotificationCompat.VisibilityPublic);
                }

                // Publish the notification:
                int notificationId = theId;
                notificationManager.Notify(notificationId, notification);

            });
        }

        public Task NotifyOldAsync(string title, string message)
        {
            //if android version is lower than Oreo we don't need to 
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                NotificationCompat.Builder bob = new NotificationCompat.Builder(_context, CHANNEL_ID)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.cert)
                .SetPriority(2);

                //build notif
                Notification notification = bob.Build();

                //get manager
                NotificationManager notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

                //publish notification 
                const int notificationId = 0;
                notificationManager.Notify(notificationId, notification);
            }

            return Task.Factory.StartNew(() =>
            {

                NotificationCompat.Builder bob = new NotificationCompat.Builder(_context, CHANNEL_ID)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Drawable.Q)
                    .SetLargeIcon((Android.Graphics.Bitmap)Resource.Drawable.Q)
                    .SetPriority(1);

                //build notif
                Notification notification = bob.Build();

                //get manager
                NotificationManager notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

                //publish notification 
                const int notificationId = 0;
                notificationManager.Notify(notificationId, notification);

            });
        }
        public void NotifyOld(string title, string message)
        {

            NotificationCompat.Builder bob = new NotificationCompat.Builder(_context, CHANNEL_ID)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Drawable.cert)
                .SetPriority(2);

            //build notif
            Notification notification = bob.Build();

            //get manager
            NotificationManager notificationManager = _context.GetSystemService(Context.NotificationService) as NotificationManager;

            //publish notification 
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);

        }
        
        public Task NotifyBigAsync(string v1, string v2, CompactWorldEvent theEvent, string theTime)
        {
            //return Task.CompletedTask;

            return Task.Factory.StartNew(() =>
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(_context, CHANNEL_ID)
                                                                           .SetSmallIcon(Resource.Drawable.planetside2logo)
                                                                           .SetContentTitle(v1)
                                                                           ;
                
                // Instantiate the Big Text style:
                //Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
                NotificationCompat.InboxStyle textStyle = new NotificationCompat.InboxStyle();
                
                //fill with text 
                textStyle.AddLine(v2);
                textStyle.AddLine($"Started at {theTime}");


                // Set the summary text:
                textStyle.SetSummaryText(v1);

                // Plug this style into the builder:
                builder.SetStyle(textStyle);

                Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    _context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    builder.SetVisibility(NotificationCompat.VisibilityPublic);
                }

                // Publish the notification:
                int notificationId = 0;
                notificationManager.Notify(notificationId, notification);

            });
        }

        public Task NotifyBigAsync(string v1, string v2)
        {
            //return Task.CompletedTask;

            return Task.Factory.StartNew(() =>
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(_context, CHANNEL_ID)
                                                                           .SetSmallIcon(Resource.Drawable.planetside2logo)
                                                                           ;
                
                // Instantiate the Big Text style:
                //Notification.BigTextStyle textStyle = new Notification.BigTextStyle();
                NotificationCompat.InboxStyle textStyle = new NotificationCompat.InboxStyle();
                
                //fill with text 
                textStyle.AddLine(v2);
                textStyle.AddLine(v2);


                // Set the summary text:
                textStyle.SetSummaryText(v1);

                // Plug this style into the builder:
                builder.SetStyle(textStyle);

                Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    _context.GetSystemService(Context.NotificationService) as NotificationManager;

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
                {
                    builder.SetVisibility(NotificationCompat.VisibilityPublic);
                }

                // Publish the notification:
                int notificationId = 0;
                notificationManager.Notify(notificationId, notification);

            });
        }
    }
}