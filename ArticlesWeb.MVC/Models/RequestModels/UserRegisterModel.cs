using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesWeb.MVC.Models.RequestModels
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string About { get; set; }
    }
}
