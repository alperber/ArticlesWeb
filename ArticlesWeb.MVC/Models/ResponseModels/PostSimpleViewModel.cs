using System;

namespace ArticlesWeb.MVC.Models.ResponseModels
{
    public class PostSimpleViewModel
    {
        public Guid Id { get; set; }
        public SimpleUserModel User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ulong CommentCount { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}