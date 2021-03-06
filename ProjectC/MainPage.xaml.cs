﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProjectC.Pages;
using ProjectC.Model;

namespace ProjectC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Guid? _currentUserId;
        protected Guid? CurrentUserId
        {
            get
            {
                return this._currentUserId.HasValue ? this._currentUserId : Boolean.Parse(Application.Current.Properties["IsLoggedIn"].ToString()) ? (Guid?)Guid.Parse(Application.Current.Properties["UserId"].ToString()) : null;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            if (this.CurrentUserId.HasValue)
            {
                btnLogin.Text = "Uitloggen";
            }
            else
            {
                btnLogin.Text = "Inloggen";
            }
        }

        //This method should be changed by one of you, like the other methods. 
        //The pagename should not be all-capital but it must start with one.
        //The Method name is *Pagename*+Button_Clicked
        //The "Clicked" method on the MainPage.xaml MUST have the same name.
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (!this.CurrentUserId.HasValue)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                Application.Current.Properties["IsLoggedIn"] = false;
                btnLogin.Text = "Inloggen";
                await DisplayAlert("Logout", "Succesvol uitgelogd", "Ok");
            }
        }

        private async void SinglePlayerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DifficultySelectPage("solo"));
        }

        private async void MultiPlayerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DifficultySelectPage("duo"));
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
        private async void HelpPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }

        private async void SettingsPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void HighScoresPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScoresPage());
        }

        private async void StatisticsPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticPage());
        }
    }
}
