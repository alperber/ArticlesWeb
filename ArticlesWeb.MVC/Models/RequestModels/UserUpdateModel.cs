using System;

namespace ArticlesWeb.MVC.Models.RequestModels
{
    public class UserUpdateModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string About { get; set; }
    }
}
