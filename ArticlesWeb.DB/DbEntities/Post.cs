using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArticlesWeb.Entities.DbEntities
{
    public class Post
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public Post()
        {
            PostId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
