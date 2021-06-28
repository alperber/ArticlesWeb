using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class CommentAddModel
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Text { get; set; }
    }
}
