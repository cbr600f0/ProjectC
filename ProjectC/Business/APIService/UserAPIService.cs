using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using ProjectC.Business.Service;
using Plugin.Connectivity;

namespace ProjectC.Business.APIService
{
    public class UserAPIService : BaseAPIService<User>
    {
        public UserAPIService(): base()
        {
        }
        public List<User> Get()
        {
            if (base.ApiIsAvailable())
            {
                return base.Get<User>().ToList();
            }
            else
            {
                return base.UserService.Get();
            }
        }

        public User Get(Guid id)
        {
            return base.Get<User>(id).SingleOrDefault();
        }

        public void AddOrUpdate(User user)
        {
            if (base.ApiIsAvailable())
            {
                base.AddOrUpdate<User>(ref user);
                user.LastSynchronized = DateTimeOffset.Now;
            }
            base.UserService.AddOrUpdate(user);
        }
    }
}
