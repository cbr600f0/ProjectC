using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectC.Business.Service;
using ProjectC.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighScoresPage : TabbedPage
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


        public HighScoresPage()
        {
            InitializeComponent();

            showMultiPlayerHighScores();
            showSinglePlayerHighScore();

        }


        private void showMultiPlayerHighScores()
        {
            List<HighScore> highScores = this.HighScoreService.GetRankedHighScores(false);
            if (highScores.Any())
            {
                multiPlayerHighScoresListView.ItemsSource = highScores;
                slMultiPlayerHighscores.IsVisible = true;
            }
            else
            {
                lblMultiPlayerHeader.Text = "Er zijn nog geen punten gehaald.";
            }

        }

        private void showSinglePlayerHighScore()
        {
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

                foreach (HighScore highScore in highScores1)
                {
                    this.HighScoreService.AddOrUpdate(highScore);
                }
            }

            if (this.CurrentUserId.HasValue)
            {
                List<HighScore> highScores = this.HighScoreService.GetRankedHighScores(true);
                if (highScores.Any())
                {
                    singlePlayerHighScoresListView.ItemsSource = highScores;
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

    }
}