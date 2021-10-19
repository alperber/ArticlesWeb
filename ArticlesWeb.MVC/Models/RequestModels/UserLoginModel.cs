using System.ComponentModel.DataAnnotations;

namespace ArticlesWeb.MVC.Models.RequestModels
{
    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
