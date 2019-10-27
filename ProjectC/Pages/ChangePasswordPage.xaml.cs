using ProjectC.Business.Service;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChangePassWordPageResource = ProjectC.Resources.ChangePasswordPage;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
        private String UserName = String.Empty;
        public ChangePasswordPage(Boolean remembersPassword, String userName)
        {
            this.UserName = userName;
            InitializeComponent();
        }

        private void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            this.ChangePassword();
        }

        private async void ChangePassword()
        {
            if (String.IsNullOrWhiteSpace(this.ePassword.Text) || String.IsNullOrWhiteSpace(eRepeatPassword.Text))
            {
                await DisplayAlert(ChangePassWordPageResource.DataMissing, ChangePassWordPageResource.EnterValidData, ChangePassWordPageResource.OK);
            }
            else if (!String.Equals(ePassword.Text, eRepeatPassword.Text))
            {
                this.ShowWarningLabel(ChangePassWordPageResource.EnterSamePassword);
                ePassword.Text = String.Empty;
                eRepeatPassword.Text = String.Empty;
            }
            else if (!this.IsValidPassword())
            {
                this.ShowWarningLabel(ChangePassWordPageResource.InvalidPassword);
            }
            else
            {
                User user = this.UserService.GetByUserName(this.UserName);
                user.Password = this.HashPassword(ePassword.Text);
                try
                {
                    this.UserService.AddOrUpdate(user);
                    await DisplayAlert(String.Empty, ChangePassWordPageResource.UpdateSuccessful, ChangePassWordPageResource.OK);
                    await Navigation.PushAsync(new LoginPage());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

        }

        private void ValidatePassword(Object sender, TextChangedEventArgs e)
        {
            if (!this.IsValidPassword())
            {
                this.ShowWarningLabel(ChangePassWordPageResource.InvalidPassword);
            }
            else
            {
                lblWarning.IsVisible = false;
            }
        }

        private Boolean IsValidPassword()
        {
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");

            return hasUpperChar.IsMatch(ePassword.Text) && hasMinimum8Chars.IsMatch(ePassword.Text);
        }

        private String HashPassword(String password)
        {
            Byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new Byte[16]);

            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, 10000);
            Byte[] hash = bytes.GetBytes(20);

            Byte[] hashBytes = new Byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        private void ShowWarningLabel(String message)
        {
            this.lblWarning.Text = message;
            this.lblWarning.TextColor = Color.IndianRed;
            this.lblWarning.IsVisible = true;
        }
    }
}