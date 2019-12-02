using ProjectC.Model;
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
    public partial class StatisticPage : ContentPage
    {
        public StatisticPage()
        {
            InitializeComponent();
            if (!BasePage.CurrentUserId.HasValue)
            {
                this.lblName.Text = "Login om je statistieken te zien!";
            }
            else
            {
                this.lblName.Text = $"Naam: {BasePage.UserService.Get(BasePage.CurrentUserId.Value).UserName}";
                List<Score> currentScores = BasePage.ScoreService.GetByUserId(BasePage.CurrentUserId.Value);
                if (!currentScores.Any())
                {
                    this.lblNoScore.IsVisible = true;
                    this.lblNoScore.Text = "Er zijn nog geen scores behaald.";
                }
                else
                {
                    this.lblAmountPlayed.IsVisible = true;
                    this.lblAmountPlayed.Text = $"Aantal keer gespeeld: {currentScores.Count}";
                    this.lblHighScore.IsVisible = true;
                    this.lblHighScore.Text = $"Highscore: {currentScores.Max(s => s.Points)}";
                    if(currentScores.Where(s => s.ManyLetters).Any())
                    {
                        this.lblHighScoreManyLetters.IsVisible = true;
                        this.lblHighScoreManyLetters.Text = $"Highscore met veel letters: {currentScores.Where(s => s.ManyLetters).Max(s => s.Points)}";
                    }
                    if (currentScores.Where(s => !s.ManyLetters).Any())
                    {
                        this.lblHighScoreLittleLetters.IsVisible = true;
                        this.lblHighScoreLittleLetters.Text = $"Highscore met weinig letters: {currentScores.Where(s => !s.ManyLetters).Max(s => s.Points)}";
                    }
                    this.lblLastScore.IsVisible = true;
                    this.lblLastScore.Text = $"Laatst behaalde score: {currentScores.OrderByDescending(s => s.CreatedAt).First().Points}";
                    this.lblBestWord.IsVisible = true;
                    this.lblBestWord.Text = $"Woord met de meeste punten: {currentScores.OrderByDescending(s => s.BestWordValue).First().BestWord}";
                    this.lblAverageScore.IsVisible = true;
                    this.lblAverageScore.Text = $"Gemiddelde score: {currentScores.Average(s => s.Points)}";
                }
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}