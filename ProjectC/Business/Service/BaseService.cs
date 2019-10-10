using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjectC.Model;
using Xamarin.Forms;
using ProjectC.Business.Interface;

namespace ProjectC.Business.Service
{
    public class BaseService<T> where T : class, BaseModel
    {
        private SQLiteConnection _SQLiteConnection;

        public BaseService()
        {
            _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _SQLiteConnection.CreateTable<User>();
        }

        public IEnumerable<T> Get<T>() where T : class, BaseModel
        {
            return _SQLiteConnection.Table<T>();
        }

        public IEnumerable<T> Get<T>(Guid id) where T : class, BaseModel
        {
            return _SQLiteConnection.Table<T>().Where(t => t.Id == id);
        }

        public void Delete<T>(Guid id) where T : class, BaseModel
        {
            _SQLiteConnection.Delete<T>(id);
        }

        public void AddOrUpdate<TModel>(ref TModel model) where TModel : BaseModel
        {
            if (this.Get<T>(model.Id).Any())
            {
                if(model.Id != null)
                {
                    model.Id = Guid.NewGuid();
                }
                _SQLiteConnection.Insert(model);
            }
            else
            {
                _SQLiteConnection.Update(model);
            }
        }
    }
}
