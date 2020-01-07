using System;
using System.Collections.Generic;
using System.Linq;

using ProjectC.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        string difficulty;
        //if(singleplayer){ alles voor singleplayer } else { alles voor mutliplayer }
        // in de singleplayer part alleen pointsp1 gebruiken (pointsp2 zal altijd 0 zijn in singleplayer)
        public GameOverPage(bool singlePlayer, string name, int points, bool manyLetters, string difficulty, string highscoreWord, int highscoreWordPoints) //pointsp1&pointsp2
        {
            switch (difficulty)
            {
                case "easy":
                    this.difficulty = "Makkelijk";
                    break;

                case "medium":
                    this.difficulty = "Gemiddeld";
                    break;

                case "hard":
                    this.difficulty = "Moeilijk";
                    break;

                case "legendary":
                    this.difficulty = "Legendarisch";
                    break;

                default:
                    break;
            }
            InitializeComponent();
            if (!BasePage.CurrentUserId.HasValue)
            {
                Name.Text = "Login om je statistieken te zien!";
            }
            else
            {
                Name.Text = $"{BasePage.UserService.Get(BasePage.CurrentUserId.Value).UserName}";
                List<Score> currentScores = BasePage.ScoreService.GetByUserId(BasePage.CurrentUserId.Value);
                Highscore.IsVisible = true;
                HighscoreText.IsVisible = true;
                Int32 highscore = currentScores.Max(s => s.Points);
                Highscore.Text = $"{(highscore == points ? "Nieuwe Highscore " : String.Empty )}{highscore}";
            }
            Points.Text = points.ToString();
            highscoreWordLabel.Text = highscoreWord + ", voor " + highscoreWordPoints.ToString() + " punten.";
            Difficulty.Text = "Je speelde op " + this.difficulty;
            Letters.Text = manyLetters ? "met veel letters" : "met weinig letters";
            Difficulty.Text = "De moeilijkheids graad is: " + this.difficulty;
            // GameOver Geluid
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Win.wav");
            player.Play();
        }

        private async void HomeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}