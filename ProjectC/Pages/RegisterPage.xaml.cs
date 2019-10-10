using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
        public RegisterPage()
        {
            var test = this.UserService.Get();
            this.InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            eUserName.ReturnCommand = new Command(() => ePassword.Focus());
            ePassword.ReturnCommand = new Command(() => eRepeatPassword.Focus());
        }
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(eUserName.Text) || String.IsNullOrWhiteSpace(ePassword.Text) || String.IsNullOrWhiteSpace(eRepeatPassword.Text))
            {
                await DisplayAlert("Enter Data", "Enter Valid Data", "OK");
            }
            else if (!string.Equals(ePassword.Text, eRepeatPassword.Text))
            {
                lblWarning.Text = "Enter Same Password";
                ePassword.Text = string.Empty;
                eRepeatPassword.Text = string.Empty;
                lblWarning.TextColor = Color.IndianRed;
                lblWarning.IsVisible = true;
            }
            else
            {
                User user = new User(eUserName.Text, ePassword.Text);
                try
                {
                    this.UserService.AddOrUpdate(user);
                    var test = this.UserService.Get();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}