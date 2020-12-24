using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class UserRegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string About { get; set; }
    }
}
