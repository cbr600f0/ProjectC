using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ProjectC.Business.Service;
using ProjectC.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighScoresPage : TabbedPage
    {
        public HighScoresPage()
        {
            InitializeComponent();

            ShowMultiPlayerHighScores();
            ShowSinglePlayerHighScore();

        }


        private void ShowMultiPlayerHighScores()
        {
            List<VMScore> scores = BasePage.ScoreAPIService.GetRankedScores();
            if (scores.Any())
            {
                multiPlayerHighScoresListView.ItemsSource = scores;
                slMultiPlayerHighscores.IsVisible = true;
            }
            else
            {
                lblMultiPlayerHeader.Text = "Er zijn nog geen punten gehaald.";
            }

        }

        private void ShowSinglePlayerHighScore()
        {
            if (BasePage.CurrentUserId.HasValue)
            {
                List<VMScore> scores = BasePage.ScoreService.GetRankedScores(true);
                if (scores.Any())
                {
                    singlePlayerHighScoresListView.ItemsSource = scores;
                    slSinglePlayerHighscores.IsVisible = true;
                }
                else
                {
                    lblSinglePlayerHeader.Text = "Je hebt nog geen punten gehaald.";
                }
            }
            else
            {
                lblSinglePlayerHeader.Text = "Login om highscores te zien!";
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}