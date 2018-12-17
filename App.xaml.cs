using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PsApp;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PsApp
{ 
    public partial class App : Application
    {
        public bool _IsDeveloper = false;

        public App()
        { 
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            
        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }


        protected override void OnSleep()
        {
            // Handle when your app sleeps

            

            //start a timer 

            //either create a job scheduler to do a
            

            //StartJobScehduler();
                //first query determines where on the 
                //call to the api that uses the 'after = [timestamp]

        }

        private void StartJobScehduler(long t)
        {
            long timeOfSleep = t;


            //get events API once every 30 / 140 seconds to check for events past         }
        }

        private async void NotifyOfEvent(object sender, EventArgs e)
        {
            var notifServ = DependencyService.Resolve<INotificationService>();
            await notifServ.NotifyAsync("test title", "message message");

        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            // destroy the job scheduler 
        }
        
    }
}