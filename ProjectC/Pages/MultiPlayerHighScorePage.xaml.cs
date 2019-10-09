using System.Collections.ObjectModel;

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

            highScoresListView.ItemsSource = HighScoreDataService.GetMultiPlayerScores();
        }
    }
}