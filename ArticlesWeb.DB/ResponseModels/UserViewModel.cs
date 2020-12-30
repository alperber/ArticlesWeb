using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Entities.ResponseModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? Birthday { get; set; }
        public int PostCount { get; set; }
        public string About { get; set; }
        public bool isAdmin { get; set; }
    }
}
