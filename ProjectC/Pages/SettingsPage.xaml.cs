using ProjectC.Model;
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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            if (BasePage.CurrentUserId.HasValue)
            {
                changePasswordButtonContainer.IsVisible = true;
            }
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {

        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void changePasswordButton_Clicked(object sender, EventArgs e)
        {
            ValidateData();
        }

        public async void ValidateData()
        {
            await Navigation.PushAsync(new ChangePasswordPage(false, BasePage.UserService.Get(BasePage.CurrentUserId.Value).UserName));
        }
    }
}