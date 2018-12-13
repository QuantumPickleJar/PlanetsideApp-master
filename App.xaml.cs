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
        }
        
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
    }
}