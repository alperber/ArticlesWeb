using System;
using System.Collections.Generic;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class PostAddModel
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
