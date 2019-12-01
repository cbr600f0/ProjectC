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
    public partial class GameOverPage : ContentPage
    {
        string name;
        public GameOverPage(string name, int points, bool manyLetters, string difficulty)
        {
            this.name = name;
            InitializeComponent();
            Name.Text = name;
            Points.Text = points.ToString();
            Letters.Text = manyLetters ? "Je hebt veel letters kunnen gebruiken" : "Je hebt weinig letters kunnen gebruiken";
            Difficulty.Text = difficulty;
        }

        private async void HomeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}