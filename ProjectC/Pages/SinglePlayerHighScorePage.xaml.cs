using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SinglePlayerHighScorePage : ContentPage
    {
        public SinglePlayerHighScorePage()
        {
            InitializeComponent();

            highScoresListView.ItemsSource = HighScoreDataService.GetSinglePlayerScores();
        }
    }
}