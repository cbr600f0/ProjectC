using Newtonsoft.Json;
using Plugin.Connectivity;
using ProjectC.Business.Interface;
using ProjectC.Business.Service;
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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC
{
    public partial class App : Application
    {
        private static readonly WebClient client = new WebClient();

        private SQLiteConnection _SQLiteConnection;

        public App()
        {
            Application.Current.Properties["IsLoggedIn"] = false;
            InitializeComponent();

            _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetConnection();

            this.SynchronizeUsers();
            this.SynchronizeScores();

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
            if(CrossConnectivity.Current.IsConnected && this.ApiIsAvailable())
            {
                List<User> usersFromAPI = BasePage.UserAPIService.Get();
                List<User> usersFromDataBase = BasePage.UserService.Get();

                this.Synchronize<User>(usersFromAPI, usersFromDataBase, new User());
            }
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
                T modelToUpdate = item;
                this.UpdateModel<T>(ref modelToUpdate, "POST");
            }

            // Items updated
            List<T> updatedItemsLocally = modelsFromDatabase.Where(m => m.ModifiedAt > m.LastSynchronized && m.Active).ToList();
            foreach(T item in updatedItemsLocally)
            {
                T modelToUpdate = item;
                this.UpdateModel<T>(ref modelToUpdate, "POST");
            }

            // Items deleted
            List<T> deletedItemsLocally = modelsFromDatabase.Where(m => m.ModifiedAt > m.LastSynchronized && !m.Active).ToList();
            foreach (T item in deletedItemsLocally)
            {
                T modelToUpdate = item;
                this.UpdateModel<T>(ref modelToUpdate, "Delete");
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

        private void UpdateModel<TModel>(ref TModel model, String method) where TModel : new()
        {
            String baseUrl = $"https://145.137.57.54:44353/api/{typeof(TModel).Name.ToLower()}";
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

            App.client.Headers[HttpRequestHeader.ContentType] = "application/json";
            App.client.UploadValuesAsync(new Uri(baseUrl), method, nvc);
        }

        private Boolean ApiIsAvailable()
        {
            String baseUrl = "https://145.137.57.54:44353/api/testconnection";
            try
            {
                WebClient client = new WebClient();
                String result = client.DownloadString(baseUrl);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
