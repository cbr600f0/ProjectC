using ProjectC.Business.Service;
using ProjectC.Helper;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RegisterPageResource = ProjectC.Resources.RegisterPage;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            this.InitializeComponent();
            eUserName.ReturnCommand = new Command(() => ePassword.Focus());
            ePassword.ReturnCommand = new Command(() => eRepeatPassword.Focus());
            foreach(String question in EnumHelper.GetIntWithDisplayNames<SecurityQuestionEnum>().Values)
            {
                pSecurityQuestion.Items.Add(question);
            }
        }
        private void RegisterButton_Clicked(Object sender, EventArgs e)
        {
            this.Register();
        }

        private async void BackButton_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void SecurityQuestionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            eSecurityQuestion.IsVisible = true;
        }

        private async void Register()
        {
            if (String.IsNullOrWhiteSpace(eUserName.Text) || String.IsNullOrWhiteSpace(ePassword.Text) || String.IsNullOrWhiteSpace(eRepeatPassword.Text))
            {
                await DisplayAlert(RegisterPageResource.DataMissing, RegisterPageResource.EnterValidData, RegisterPageResource.OK);
            }
            else if (!String.Equals(ePassword.Text, eRepeatPassword.Text))
            {
                this.ShowWarningLabel(RegisterPageResource.EnterSamePassword);
                ePassword.Text = String.Empty;
                eRepeatPassword.Text = String.Empty;
            }
            else if (!this.IsValidPassword())
            {
                this.ShowWarningLabel(RegisterPageResource.InvalidPassword);
            }
            else if (BasePage.UserService.Get().Select(u => u.UserName).Contains(eUserName.Text))
            {
                this.ShowWarningLabel(RegisterPageResource.UserNameExists);
                eUserName.Text = String.Empty;
            }
            else
            {
                User user = new User(eUserName.Text, this.HashPassword(ePassword.Text), (SecurityQuestionEnum)pSecurityQuestion.SelectedIndex, eSecurityQuestion.Text);
                try
                {
                    BasePage.UserAPIService.AddOrUpdate(user);
                    await DisplayAlert(String.Empty, RegisterPageResource.RegisterSuccessful, RegisterPageResource.OK);
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
                this.ShowWarningLabel(RegisterPageResource.InvalidPassword);
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
            lblWarning.Text = message;
            lblWarning.TextColor = Color.IndianRed;
            lblWarning.IsVisible = true;
        }
    }
}