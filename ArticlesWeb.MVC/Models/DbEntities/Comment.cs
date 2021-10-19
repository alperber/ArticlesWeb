using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ArticlesWeb.MVC.Models.DbEntities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }

        public Comment()
        {
            CreatedAt = DateTimeOffset.Now;
        }
    }
}
