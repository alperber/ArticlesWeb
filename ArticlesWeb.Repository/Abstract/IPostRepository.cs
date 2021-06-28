using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Entities.DbEntities;

namespace ArticlesWeb.Repository.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Post GetPostWithUser(string postId);
        List<Post> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null);
        void IncrementCommentCount(string postId);
        void DecrementCommentCount(string postId);
    }
}
