using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProjectC.Pages;

namespace ProjectC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MIJNPAGINAISWHATEVER());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {

        }
    }
}
