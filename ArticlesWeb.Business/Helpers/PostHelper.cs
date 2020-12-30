using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Helpers
{
    public static class PostHelper
    {
        public static Post CreatePost(PostAddModel post)
        {
            return new Post()
            {
                UserId = post.UserId,
                CommentCount = 0,
                CreatedAt = DateTime.Now,
                Text = post.Text,
                Title = post.Title
            };
        }
    }
}
