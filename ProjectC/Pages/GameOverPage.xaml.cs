using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOverPage : ContentPage
    {
        string difficulty;
        string name;
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
            switch (name)
            {
                case "Je bent niet ingelogd":
                    this.name = "Log in om je highscore op te slaan";
                    break;

                default:
                    this.name = name;
                    break;
            }
            InitializeComponent();
            Name.Text = name;
            Points.Text = points.ToString();
            highscoreWordLabel.Text = highscoreWord + ", voor " + highscoreWordPoints.ToString() + " punten.";
            Difficulty.Text = "Je speelde op " + this.difficulty;
            Letters.Text = manyLetters ? "met veel letters" : "met weinig letters";
        }

        private async void HomeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}