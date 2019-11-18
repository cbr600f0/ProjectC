using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC
{
    public partial class App : Application
    {
        public App()
        {
            Application.Current.Properties["IsLoggedIn"] = false;
            InitializeComponent();

            var splashPage = new NavigationPage(new SplashScreen());
            MainPage = splashPage;
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
