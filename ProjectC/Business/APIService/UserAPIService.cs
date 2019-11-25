using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using ProjectC.Business.Service;

namespace ProjectC.Business.APIService
{
    public class UserAPIService : BaseAPIService<User>
    {
        public UserAPIService(): base()
        {
        }
        public List<User> Get()
        {
            return base.Get<User>().ToList();
        }

        public User Get(Guid id)
        {
            return base.Get<User>(id).SingleOrDefault();
        }
    }
}
