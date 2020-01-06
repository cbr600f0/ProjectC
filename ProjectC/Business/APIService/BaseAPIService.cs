using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjectC.Model;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using SQLite;
using System.Net.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ProjectC.Business.Service;
using System.Collections.Specialized;
using ProjectC.Helper;

namespace ProjectC.Business.APIService
{
    public class BaseAPIService<T> where T : class, BaseModel, new()
    {
        #region Services

        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }
        private ScoreService _scoreService;
        protected ScoreService ScoreService
        {
            get
            {
                return this._scoreService = this._scoreService ?? new ScoreService();
            }
        }

        #endregion
        public BaseAPIService()
        {
        }

        public IEnumerable<TModel> Get<TModel>() where TModel : new()
        {
            return this.GetModelFromApi<TModel>();
        }

        public IEnumerable<TModel> Get<TModel>(Guid id) where TModel : BaseModel , new()
        {
            return this.GetModelFromApi<TModel>()
                .Where(m => m.Id == id);
        }

        public void Delete<TModel>(Guid id) where TModel : class, BaseModel
        {
            this.DeleteModel<TModel>(id);
        }

        public Guid AddOrUpdate<TModel>(ref TModel model) where TModel : BaseModel, new()
        {
            this.SetBaseProperties(ref model);
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            this.UpdateModel<TModel>(ref model);

            return model.Id;
        }

        internal void SetBaseProperties<TModel>(ref TModel model) where TModel : BaseModel, new()
        {
            if (model.CreatedAt != DateTimeOffset.MinValue)
            {
                model.ModifiedAt = DateTimeOffset.Now;
            }
            else
            {
                model.CreatedAt = DateTimeOffset.Now;
                model.Active = true;
            }
        }

        public Boolean ApiIsAvailable()
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

        private IEnumerable<TModel> GetModelFromApi<TModel>() where TModel : new()
        {
            String baseUrl = $"{App.BaseUrl}{typeof(T).Name.ToLower()}";

            WebClient client = new WebClient();
            String result = client.DownloadString(baseUrl);

            return JsonConvert.DeserializeObject<List<TModel>>(result);
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

        private void DeleteModel<TModel>(Guid id) where TModel : class, BaseModel
        {
            String url = $"{App.BaseUrl}{typeof(TModel).Name.ToLower()}";

            App.client.Headers[HttpRequestHeader.ContentType] = "application/json";
            App.client.UploadStringAsync(new Uri(url), "Delete", id.ToString());
        }
    }
}
