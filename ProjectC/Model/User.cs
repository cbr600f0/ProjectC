using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public class User : BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public Boolean Active { get; set; }

        public DateTimeOffset? LastSynchronized { get; set; }

        public String UserName { get; set; }
        
        public String Password { get; set; }

        public SecurityQuestionEnum SecurityQuestion { get; set; }

        public String SecurityQuestionAnswer { get; set; }

        public String HighScore { get; set; }

        public User()
        {
        }

        public User(String userName, String password, SecurityQuestionEnum securityQuestion, String securityQuestionAnswer)
        {
            this.UserName = userName;
            this.Password = password;
            this.SecurityQuestion = securityQuestion;
            this.SecurityQuestionAnswer = securityQuestionAnswer;
        }
    }
}
