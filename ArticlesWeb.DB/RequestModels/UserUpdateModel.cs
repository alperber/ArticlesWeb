using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class UserUpdateModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string About { get; set; }
    }
}
