using Newtonsoft.Json;
using Plugin.Connectivity;
using ProjectC.Business.Interface;
using ProjectC.Business.Service;
using ProjectC.Helper;
using ProjectC.Model;
using ProjectC.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC
{
    public partial class App : Application
    {
        public static readonly WebClient client = new WebClient();

        public static readonly NetworkAccess connectivity = Connectivity.NetworkAccess;  

        public static readonly String BaseUrl = "https://145.137.57.69:44353/api/";

        public App()
        {
            Application.Current.Properties["IsLoggedIn"] = false;

            if (App.connectivity == NetworkAccess.Internet && this.ApiIsAvailable())
            {
                this.SynchronizeUsers();
                this.SynchronizeScores();
            }

            InitializeComponent();
            var splashPage = new NavigationPage(new SplashScreen());
            MainPage = splashPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SynchronizeUsers()
        {
            List<User> usersFromAPI = BasePage.UserAPIService.Get();
            List<User> usersFromDataBase = BasePage.UserService.Get();

            this.Synchronize<User>(usersFromAPI, usersFromDataBase, new User());
        }

        private void SynchronizeScores()
        {
            List<Score> scoresFromAPI = BasePage.ScoreAPIService.Get();
            List<Score> scoresFromDataBase = BasePage.ScoreService.Get();
        }

        private void Synchronize<T>(List<T> modelsFromAPI, List<T> modelsFromDatabase, T model) where T : class, BaseModel, new()
        {
            BaseService<T> service = new BaseService<T>();

            // Todo: make batch functions for a better performance
            #region Local to Global

            // New items added
            List<T> addedItemsLocally = modelsFromDatabase.Where(m => m.LastSynchronized == null).ToList();
            List<T> modelsToAdd = new List<T>();
            foreach(T item in addedItemsLocally)
            {
                this.Delete<T>(item.Id);
            }

            // Items updated
            List<T> updatedItemsLocally = modelsFromDatabase.Where(m => m.ModifiedAt > m.LastSynchronized && m.Active).ToList();
            foreach(T item in updatedItemsLocally)
            {
                T modelToUpdate = item;
                this.UpdateModel<T>(ref modelToUpdate);
            }

            // Items deleted
            List<T> deletedItemsLocally = modelsFromDatabase.Where(m => m.ModifiedAt > m.LastSynchronized && !m.Active).ToList();
            foreach (T item in deletedItemsLocally)
            {
                this.Delete<T>(item.Id);
            }

            #endregion

            #region Global to Local

            // New items added
            List<Guid> addedIdsGlobally = modelsFromAPI.Select(m => m.Id).Except(modelsFromDatabase.Select(m => m.Id)).ToList();
            List<T> addedItemsGlocally = modelsFromAPI.Where(m => addedIdsGlobally.Contains(m.Id)).ToList();

            foreach(T item in addedItemsGlocally)
            {
                T modelToUpdate = item;
                service.AddOrUpdate(ref modelToUpdate);
            }

            // Items updated

            // Items deleted


            #endregion

            // BatchUpdate LastSynchronized
            if (modelsFromDatabase.Any())
            {
                service.BatchUpdateLastSynchronized<T>(modelsFromDatabase.Select(m => m.Id).ToList(), ref model);
            }
        }

        private void UpdateModel<TModel>(ref TModel model) where TModel : new()
        {
            String url = $"{App.BaseUrl}{typeof(TModel).Name.ToLower()}";
            String jsonData = JsonConvert.SerializeObject(model);

            Dictionary<String, String> dict = JsonConvert.DeserializeObject<Dictionary<String, String>>(jsonData);

            NameValueCollection nvc = null;
            if (dict != null)
            {
                nvc = new NameValueCollection(dict.Count);
                foreach (var k in dict)
                {
                    nvc.Add(k.Key, k.Value);
                }
            }

            App.client.UploadValues(new Uri(url), nvc);
        }

        private void Delete<TModel>(Guid id) where TModel : new()
        {
            String url = $"{App.BaseUrl}{typeof(TModel).Name.ToLower()}";

            App.client.Headers[HttpRequestHeader.ContentType] = "application/json";
            App.client.UploadStringAsync(new Uri(url), "Delete", id.ToString());
        }

        private Boolean ApiIsAvailable()
        {
            String url = $"{App.BaseUrl}testconnection";

            using (WebClient webClient = new WebCientTimeOut())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Proxy = new WebProxy();
                try
                {
                    String response = webClient.DownloadString(url);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
