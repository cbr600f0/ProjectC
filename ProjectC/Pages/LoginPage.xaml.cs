using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LoginPageResource = ProjectC.Resources.LoginPage;

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
            List<User> users = this.UserService.Get();
            this.InitializeComponent();
            eUserName.ReturnCommand = new Command(() => ePassword.Focus());
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            this.Login();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void ForgotPasswordButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        private async void Login()
        {
            List<User> users = this.UserService.Get();

            if (users.Where(u => u.UserName == eUserName.Text && this.ValidatePassword(u.Password, ePassword.Text)).Any())
            {
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
                Application.Current.Properties["UserId"] = users.Where(u => u.UserName == eUserName.Text).Select(u => u.Id).Single();
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                this.ShowWarningLabel(LoginPageResource.UserNameAndOrPasswordIncorrect);
            }
        }

        private Boolean ValidatePassword(String savedPassword, String password)
        {
            Byte[] hashBytes = Convert.FromBase64String(savedPassword);

            Byte[] salt = new Byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, 10000);
            Byte[] hash = bytes.GetBytes(20);

            for (Int32 i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void ShowWarningLabel(String message)
        {
            lblWarning.Text = message;
            lblWarning.TextColor = Color.IndianRed;
            lblWarning.IsVisible = true;
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }
    }
}