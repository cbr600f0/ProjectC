using ProjectC.Business.Service;
using ProjectC.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiPlayerHighScorePage : ContentPage
    {
        public MultiPlayerHighScorePage()
        {
            InitializeComponent();
            List<Score> highScores = BasePage.ScoreService.GetRankedScores(false);
            if (highScores.Any())
            {
                lvHighscores.ItemsSource = highScores;
                slHighscores.IsVisible = true;
            }
            else
            {
                lblHeader.Text = "Er zijn nog geen punten gehaald.";
            }
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}