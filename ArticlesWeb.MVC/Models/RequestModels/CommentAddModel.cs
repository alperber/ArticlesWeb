using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesWeb.MVC.Models.RequestModels
{
    public class CommentAddModel
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
    }
}
