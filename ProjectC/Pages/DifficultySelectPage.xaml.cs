using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DifficultySelectPage : ContentPage
    {
        bool DifficultyHasBeenPicked = false;
        int WordRowsDifficultymultiplier = 2;
        public DifficultySelectPage()
        {
            InitializeComponent();
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
                await Navigation.PopAsync();
        }
        private async void EasyButton_Clicked(object sender, EventArgs e)
        {
            if (DifficultyHasBeenPicked)
            {
                await Navigation.PushAsync(new SinglePlayerPage("easy", WordRowsDifficultymultiplier));
            }
            else
            {
                lblWarning.IsVisible = true;
            }
        }
        private async void MediumButton_Clicked(object sender, EventArgs e)
        {
            if (DifficultyHasBeenPicked)
            {
                await Navigation.PushAsync(new SinglePlayerPage("medium", WordRowsDifficultymultiplier));
            }
            else
            {
                lblWarning.IsVisible = true;
            }
        }
        private async void HardButton_Clicked(object sender, EventArgs e)
        {
            if (DifficultyHasBeenPicked)
            {
                await Navigation.PushAsync(new SinglePlayerPage("hard", WordRowsDifficultymultiplier));
            }
            else
            {
                lblWarning.IsVisible = true;
            }
        }
        private async void LegendButton_Clicked(object sender, EventArgs e)
        {
            if (DifficultyHasBeenPicked)
            {
                await Navigation.PushAsync(new SinglePlayerPage("legendary", WordRowsDifficultymultiplier));
            }
            else
            {
                lblWarning.IsVisible = true;
            }
        }
        private void EasyDifficultySelector(object sender, EventArgs e)
        {
            DifficultyHasBeenPicked = true;
            WordRowsDifficultymultiplier = 3;
            lblWarning.IsVisible = false;
            easyLettersButton.BackgroundColor = Color.Green;
            hardLettersButton.BackgroundColor = Color.Gray;
        }

        private void HardDifficultySelector(object sender, EventArgs e)
        {
            DifficultyHasBeenPicked = true;
            WordRowsDifficultymultiplier = 2;
            lblWarning.IsVisible = false;
            easyLettersButton.BackgroundColor = Color.Gray;
            hardLettersButton.BackgroundColor = Color.Green;
        }
    }
}