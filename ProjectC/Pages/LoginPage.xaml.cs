using ProjectC.Business.Service;
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
    public partial class LoginPage : ContentPage
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
        public LoginPage()
        {
            this.InitializeComponent();
            eUserName.ReturnCommand = new Command(() => ePassword.Focus());
        }
        //Works like this temporarily. Until we have a functioning login program. 
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            List<User> users = this.UserService.Get();

            if (users.Where(u => u.UserName == eUserName.Text && u.Password == ePassword.Text).Any())
            {
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                Application.Current.Properties["UserId"] = users.Where(u => u.UserName == eUserName.Text).Select(u => u.Id).Single();
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                lblWarning.Text = "Gebruikersnaam en/of wachtwoord is incorrect";
                lblWarning.TextColor = Color.IndianRed;
                lblWarning.IsVisible = true;
            }
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}