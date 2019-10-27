using System.Collections.ObjectModel;
using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
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
        public SinglePlayerHighScorePage()
        {
            this.InitializeComponent();

            highScoresListView.ItemsSource = HighScoreDataService.GetSinglePlayerScores();
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}