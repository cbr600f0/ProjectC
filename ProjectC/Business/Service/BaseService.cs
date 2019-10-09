using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjectC.Model;

namespace ProjectC.Business.Service
{
    public class BaseService<T> where T : class, BaseModel
    {
        private SQLiteConnection _SQLiteConnection;

        public IEnumerable<T> Get()
        {
            return _SQLiteConnection.Table<T>();
        }

        public IEnumerable<T> Get(Guid id)
        {
            return _SQLiteConnection.Table<T>().Where(t => t.Id == id);
        }

        public void Delete(Guid id)
        {
            _SQLiteConnection.Delete<T>(id);
        }

        public void AddOrUpdate<TModel>(ref TModel model) where TModel : BaseModel
        {
            if (this.Get(model.Id).Any())
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
