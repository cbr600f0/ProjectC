﻿using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite.Net;
using Xamarin.Forms;
using ProjectC.Business.Interface;

namespace ProjectC.Business.Service
{
    public class UserService : BaseService<User>
    {
        public UserService(): base()
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

        public void Delete(Guid id)
        {
            base.Delete<User>(id);
        }
        
        public void AddOrUpdate(User user)
        {
            try
            {
                base.AddOrUpdate(ref user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean ValidateLogin(String userName, String passWord)
        {
            return this.Get()
                .Where(u => u.UserName == userName && u.Password == passWord)
                .Any();
        }
    }
}
