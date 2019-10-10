using ProjectC.Business.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectC.Pages
{
    public class BasePage : ContentPage
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
    }
}
