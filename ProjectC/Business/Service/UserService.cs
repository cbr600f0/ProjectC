using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ProjectC.Business.Service
{
    class UserService : BaseService<User>
    {
        public List<User> Get()
        {
            return base.Get().ToList();
        }

        public User Get(Guid id)
        {
            return base.Get(id).SingleOrDefault();
        }

        public void Delete(Guid id)
        {
            base.Delete(id);
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
            return base.Get()
                .Where(u => u.UserName == userName && u.Password == passWord)
                .Any();
        }
    }
}
