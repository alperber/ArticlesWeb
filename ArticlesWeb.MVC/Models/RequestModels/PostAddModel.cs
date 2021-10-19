using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesWeb.MVC.Models.RequestModels
{
    public class PostAddModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
