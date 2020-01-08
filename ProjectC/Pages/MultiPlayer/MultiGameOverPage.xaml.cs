using System;
using System.Collections.Generic;
using System.Linq;

using ProjectC.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages.MultiPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiGameOverPage : ContentPage
    {
        string difficulty;
        //if(singleplayer){ alles voor singleplayer } else { alles voor mutliplayer }
        // in de singleplayer part alleen pointsp1 gebruiken (pointsp2 zal altijd 0 zijn in singleplayer)
        public MultiGameOverPage(bool singlePlayer, string name, int pointsp1, bool manyLetters, string difficulty, string highscoreWord, int highscoreWordPoints, int pointsp2) //pointsp1&pointsp2
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
            Points1.Text = pointsp1.ToString();
            Points2.Text = pointsp2.ToString();
            if (pointsp1 == pointsp2)
            {
                Name.Text = "Gelijk gespeeld!";
            }
            else if (pointsp1 > pointsp2)
            {
                Name.Text = "Winnaar: Speler 1!";
                Name.TextColor = Color.FromHex("#005D4E");
                Name.BackgroundColor = Color.FromHex("#77D4C4");
            }
            else if (pointsp1 < pointsp2)
            {
                Name.Text = "Winnaar: Speler 2!";
                Name.TextColor = Color.FromHex("#839100");
                Name.BackgroundColor = Color.FromHex("#FBFFDE");
            }

            highscoreWordLabel1.Text = highscoreWord + ", voor " + highscoreWordPoints.ToString() + " punten.";
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