using System;
using System.Collections.Generic;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.ResponseModels;

namespace ArticlesWeb.MVC.Models.ResponseModels
{
    public class PostDetailedViewModel
    {
        public Guid Id { get; set; }
        public SimpleUserModel User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ulong CommentCount { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}