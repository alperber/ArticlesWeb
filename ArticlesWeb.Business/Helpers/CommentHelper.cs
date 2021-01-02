using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Helpers
{
    public static class CommentHelper
    {
        public static Comment CreateComment(CommentAddModel comment)
        {
            return new Comment()
            {
                CreatedAt = DateTime.Now,
                UserId = comment.UserId,
                Text = comment.Text,
                PostId = comment.PostId
            };
        }
    }
}
