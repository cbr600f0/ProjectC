using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public class User : BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public String UserName { get; set; }

        [MaxLength(12)]
        public String Password { get; set; }

        public String HighScore { get; set; }

        public User()
        {
        }

        public User(String userName, String password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
