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
    public partial class DifficultySelectScreen : ContentPage
    {
        bool _difficultyHasBeenPicked = false;
        int wordRowsDifficultymultiplier = 2;
        public DifficultySelectScreen()
        {
            InitializeComponent();

            easyButton.IsEnabled = false;
            mediumButton.IsEnabled = false;
            hardButton.IsEnabled = false;
            legendaryButton.IsEnabled = false;
        }

        private async void MakkelijkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("easy", wordRowsDifficultymultiplier));
        }
        private async void NormaalButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("medium", wordRowsDifficultymultiplier));
        }
        private async void MoeilijkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("hard", wordRowsDifficultymultiplier));
        }
        private async void LegendarischButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("legendary", wordRowsDifficultymultiplier));
        }

        private void EasyDifficultySelector(object sender, EventArgs e)
        {
            difficultyHasBeenPicked = true;
            wordRowsDifficultymultiplier = 3;
            easyLettersButton.BackgroundColor = Color.FromHex("B6F287");
            hardLettersButton.BackgroundColor = Color.FromHex("B0B0B0");
        }

        private void HardDifficultySelector(object sender, EventArgs e)
        {
            difficultyHasBeenPicked = true;
            wordRowsDifficultymultiplier = 2;
            easyLettersButton.BackgroundColor = Color.FromHex("B0B0B0");
            hardLettersButton.BackgroundColor = Color.FromHex("B6F287");
        }

        public bool difficultyHasBeenPicked
        {
            get { return _difficultyHasBeenPicked; }
            set
            {
                _difficultyHasBeenPicked = value;
                if (_difficultyHasBeenPicked)
                {
                    easyButton.BackgroundColor = Color.FromHex("B6F287");
                    mediumButton.BackgroundColor = Color.FromHex("B6F287");
                    hardButton.BackgroundColor = Color.FromHex("B6F287");
                    legendaryButton.BackgroundColor = Color.FromHex("B6F287");

                    easyButton.IsEnabled = true;
                    mediumButton.IsEnabled = true;
                    hardButton.IsEnabled = true;
                    legendaryButton.IsEnabled = true;
                }
                else if(!_difficultyHasBeenPicked)
                {
                    easyButton.BackgroundColor = Color.FromHex("EEF0ED");
                    mediumButton.BackgroundColor = Color.FromHex("EEF0ED");
                    hardButton.BackgroundColor = Color.FromHex("EEF0ED");
                    legendaryButton.BackgroundColor = Color.FromHex("EEF0ED");
                }
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}