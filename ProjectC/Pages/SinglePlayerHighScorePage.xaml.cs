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
        private ScoreService _scoreService;
        protected ScoreService ScoreService
        {
            get
            {
                return this._scoreService = this._scoreService ?? new ScoreService();
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
            InitializeComponent();
            if (this.CurrentUserId.HasValue)
            {
                List<Score> highScores = this.ScoreService.GetRankedScores(true);
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