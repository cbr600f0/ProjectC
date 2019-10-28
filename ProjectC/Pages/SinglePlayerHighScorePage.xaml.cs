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
        private HighScoreService _highScoreService;
        protected HighScoreService HighScoreService
        {
            get
            {
                return this._highScoreService = this._highScoreService ?? new HighScoreService();
            }
        }

        private Guid? _currentUserId;
        protected Guid? CurrentUserId
        {
            get
            {
                return this._currentUserId.HasValue ? this._currentUserId : Boolean.Parse(Application.Current.Properties["IsLoggedIn"].ToString()) ? (Guid?)Guid.Parse(Application.Current.Properties["UserId"].ToString()) : null;
            }
        }

        public SinglePlayerHighScorePage()
        {
            //test data
            if (this.CurrentUserId.HasValue && !this.HighScoreService.Get().Any())
            {
                List<HighScore> highScores1 = new List<HighScore>() 
                {
                    new HighScore(this.CurrentUserId.Value, 100, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 3435, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 24234, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 2323, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 55, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 6, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 6252, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 51, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 413424, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 1234, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 431, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 500, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 999, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 9990, DateTimeOffset.Now),
                    new HighScore(this.CurrentUserId.Value, 343, DateTimeOffset.Now),
                };

                foreach(HighScore highScore in highScores1)
                {
                    this.HighScoreService.AddOrUpdate(highScore);
                }
            }

            InitializeComponent();
            if (this.CurrentUserId.HasValue)
            {
                List<HighScore> highScores = this.HighScoreService.GetRankedHighScores(true);
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