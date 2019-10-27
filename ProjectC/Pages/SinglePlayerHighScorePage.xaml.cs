using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

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
                return this._currentUserId.HasValue ? this._currentUserId : Application.Current.Properties.ContainsKey("UserId") ? (Guid?)Guid.Parse(Application.Current.Properties["UserId"].ToString()) : null;
            }
        }

        public SinglePlayerHighScorePage()
        {
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
    }
}