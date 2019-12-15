using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        string difficulty;
        public GameOverPage(string name, int points, bool manyLetters, string difficulty, string highscoreWord, int highscoreWordPoints)
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
            Name.Text = name;
            Points.Text = "Je totale aantal punten is: " + points.ToString();
            highscoreWordLabel.Text = "Je hoogste woord is: " + highscoreWord;
            highscoreWordPointsLabel.Text = "De punten van je hoogste woord is: " + highscoreWordPoints.ToString();
            Letters.Text = manyLetters ? "beschikbare letters: veel" : "beschikbare letters: weinig";
            Difficulty.Text = "De moeilijkheids graad is: " + this.difficulty;
            // GameOver Geluid
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load("Win.wav");
            player.Play();

        }

        private async void HomeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}