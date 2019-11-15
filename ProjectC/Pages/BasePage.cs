using ProjectC.Business.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectC.Pages
{
    public class BasePage : ContentPage
    {
        private static Guid? _currentUserId;
        public static Guid? CurrentUserId
        {
            get
            {
                return BasePage._currentUserId.HasValue ? BasePage._currentUserId : Boolean.Parse(Application.Current.Properties["IsLoggedIn"].ToString()) ? (Guid?)Guid.Parse(Application.Current.Properties["UserId"].ToString()) : null;
            }
        }

        private static UserService _userService;
        public static UserService UserService
        {
            get
            {
                return BasePage._userService = BasePage._userService ?? new UserService();
            }
        }

        private static ScoreService _scoreService;
        public static ScoreService ScoreService
        {
            get
            {
                return BasePage._scoreService = BasePage._scoreService ?? new ScoreService();
            }
        }
    }
}
