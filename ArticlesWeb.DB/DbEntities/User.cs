using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArticlesWeb.Entities.DbEntities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? Birthday { get; set; }
        public int PostCount { get; set; }
        public string About { get; set; }
        public bool isAdmin { get; set; }
    }
}
