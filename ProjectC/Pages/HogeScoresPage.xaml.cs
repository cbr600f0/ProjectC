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
        public SinglePlayerHighScorePage()
        {
            InitializeComponent();
            if (BasePage.CurrentUserId.HasValue)
            {
                List<Score> highScores = BasePage.ScoreService.GetRankedScores(true);
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