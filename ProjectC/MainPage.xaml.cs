using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProjectC.Pages;

namespace ProjectC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //This method should be changed by one of you, like the other methods. 
        //The pagename should not be all-capital but it must start with one.
        //The Method name is *Pagename*+Button_Clicked
        //The "Clicked" method on the MainPage.xaml MUST have the same name.
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new login());
        }

        private async void SinglePlayerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayer());
        }

        private async void MultiPlayerButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MultiPlayer());
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }
        private async void HelpPageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }

        private async void SinglePlayerHighScoreButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerHighScorePage());
        }

        private async void MultiPlayerHighScoreButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MultiPlayerHighScorePage());
        }
    }
}
