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
        public DifficultySelectScreen()
        {
            InitializeComponent();
        }

        private async void MakkelijkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("easy"));
        }
        private async void NormaalButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("medium"));
        }
        private async void MoeilijkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("hard"));
        }
        private async void LegendarischButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SinglePlayerPage("legendary"));
        }
    }
}