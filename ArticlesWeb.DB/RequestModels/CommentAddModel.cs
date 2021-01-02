using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class CommentAddModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
    }
}
