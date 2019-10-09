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

        public String EmailAdress { get; set; }
        
        public User()
        {
        }
    }
}
