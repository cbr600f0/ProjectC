using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProjectC.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DifficultySelectPage : ContentPage
    {
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
            await Navigation.PushAsync(new SinglePlayerPage("easy"));
        }
        private async void MediumButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("medium"));
        }
        private async void HardButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("hard"));
        }
        private async void LegendButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("legendary"));
        }
    }
}