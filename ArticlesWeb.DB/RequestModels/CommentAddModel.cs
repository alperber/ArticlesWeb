using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class CommentAddModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Text { get; set; }
    }
}
