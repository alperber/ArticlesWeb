using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArticlesWeb.Entities.DbEntities
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Comment()
        {
            CommentId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }
    }
}
