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

namespace ProjectC.Business.APIService
{
    public class BaseAPIService<T> where T : class, BaseModel, new()
    {
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

        private IEnumerable<TModel> GetModelFromApi<TModel>() where TModel : new()
        {
            String baseUrl = $"https://145.137.57.188:44353/api/{typeof(T).Name.ToLower()}";

            WebClient client = new WebClient();
            String result = client.DownloadString(baseUrl);

            return JsonConvert.DeserializeObject<List<TModel>>(result);
        }
    }
}
