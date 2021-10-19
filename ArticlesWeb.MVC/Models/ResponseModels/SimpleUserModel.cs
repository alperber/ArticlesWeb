using System;

namespace ArticlesWeb.MVC.Models.ResponseModels
{
    public class SimpleUserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}