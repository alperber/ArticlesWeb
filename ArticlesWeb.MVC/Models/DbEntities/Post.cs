using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArticlesWeb.MVC.Models.DbEntities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ulong CommentCount { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public IList<Comment> Comments { get; set; }

        public Post()
        {
            CreatedAt = DateTimeOffset.Now;
        }
    }
}
