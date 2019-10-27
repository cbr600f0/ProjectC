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
    public partial class MultiPlayerPage : ContentPage
    {
        public MultiPlayerPage()
        {
            InitializeComponent();
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}