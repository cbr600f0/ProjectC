﻿using ProjectC.Business.Service;
using ProjectC.Helper;
using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ForgotPasswordPageResource = ProjectC.Resources.ForgotPasswordPage;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
        public ForgotPasswordPage()
        {
            InitializeComponent();
            foreach (String question in EnumHelper.GetIntWithDisplayNames<SecurityQuestionEnum>().Values)
            {
                pSecurityQuestion.Items.Add(question);
            }
        }

        private void SecurityQuestionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            eSecurityQuestion.IsVisible = true;
        }

        private void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            this.ValidateData();
        }

        private async void ValidateData()
        {
            List<User> users = this.UserService.Get();

            if(users.Where(u => u.UserName == eUserName.Text && u.SecurityQuestion == (SecurityQuestionEnum)pSecurityQuestion.SelectedIndex && u.SecurityQuestionAnswer == eSecurityQuestion.Text).Any())
            {
                await Navigation.PushAsync(new ChangePasswordPage(false, eUserName.Text));
            }
            else
            {
                this.ShowWarningLabel(ForgotPasswordPageResource.UserNameAndOrSecurityQuestionIncorrect);
            }
        }

        private void ShowWarningLabel(String message)
        {
            lblWarning.Text = message;
            lblWarning.TextColor = Color.IndianRed;
            lblWarning.IsVisible = true;
        }
    }
}