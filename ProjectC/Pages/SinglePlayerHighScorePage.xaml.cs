using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SinglePlayerHighScorePage : ContentPage
    {
        public SinglePlayerHighScorePage()
        {
            InitializeComponent();
            if (BasePage.CurrentUserId.HasValue)
            {
                List<Score> highScores = BasePage.ScoreService.GetRankedScores(true);
                if (highScores.Any())
                {
                    lvHighscores.ItemsSource = highScores;
                    slHighscores.IsVisible = true;
                }
                else
                {
                    lblHeader.Text = "Je hebt nog geen punten gehaald.";
                }
            }
            else
            {
                lblHeader.Text = "Login om highscores te zien!";
            }
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}