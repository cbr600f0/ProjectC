using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjectC.Model;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using SQLite;

namespace ProjectC.Business.Service
{
    public class BaseService<T> where T : class, BaseModel, new()
    {
        private Guid? _currentUserId;
        protected Guid? CurrentUserId
        {
            get
            {
                return this._currentUserId.HasValue ? this._currentUserId : Boolean.Parse(Application.Current.Properties["IsLoggedIn"].ToString()) ? (Guid?)Guid.Parse(Application.Current.Properties["UserId"].ToString()) : null;
            }
        }

        private SQLiteConnection _SQLiteConnection;

        public BaseService()
        {
            _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _SQLiteConnection.CreateTable<User>();
            _SQLiteConnection.CreateTable<HighScore>();
        }

        public IEnumerable<TModel> Get<TModel>() where TModel : new()
        {
            return _SQLiteConnection.Table<TModel>();
        }

        public IEnumerable<TModel> Get<TModel>(Guid id) where TModel : BaseModel , new()
        {
            return _SQLiteConnection.Table<TModel>().Where(t => t.Id == id);
        }

        public void Delete<TModel>(Guid id) where TModel : class, BaseModel
        {
            _SQLiteConnection.Delete<TModel>(id);
        }

        public void AddOrUpdate<TModel>(ref TModel model) where TModel : BaseModel, new()
        {
            if (model.Id == null || model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            if (!this.Get<TModel>(model.Id).Any())
            {
                _SQLiteConnection.Insert(model);
            }
            else
            {
                _SQLiteConnection.Update(model);
            }
        }
    }
}
