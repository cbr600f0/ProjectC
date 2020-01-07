using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjectC.Model;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using SQLite;
using System.Data.SqlClient;

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
            _SQLiteConnection.CreateTable<Score>();
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
            this.SetBaseProperties(ref model);
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

        internal void SetBaseProperties<TModel>(ref TModel model) where TModel : BaseModel, new()
        {
            if (model.CreatedAt != DateTimeOffset.MinValue && model.LastSynchronized != null)
            {
                model.ModifiedAt = DateTimeOffset.Now;
            }
            else
            {
                model.CreatedAt = DateTimeOffset.Now;
                model.Active = true;
            }
        }

        public void BatchUpdateLastSynchronized<TModel>(List<Guid> ids, ref TModel model) where TModel : BaseModel, new()
        {
            List<String> columnNameAndEqualsParameter = new List<String>();

            columnNameAndEqualsParameter.Add(String.Format("[{0}] = '{1}'", "LastSynchronized", DateTimeOffset.Now));

            List<String> idParameters = ids.Select(id => String.Format("'{0}'", id)).ToList();

            String command = String.Empty;

            for (Int32 i = 0; i < idParameters.Count; i += 1000)
            {
                String equalsString = String.Join(", ", columnNameAndEqualsParameter.ToArray());
                String where = String.Format("Id in ({0})", String.Join(", ", idParameters.Skip(i).Take(1000).ToArray()));

                command = String.Format("{0} update {1} set {2} where {3}", command, typeof(T).Name, equalsString, where);
            }

            this._SQLiteConnection.CreateCommand(command).ExecuteNonQuery();
        }
    }
}
