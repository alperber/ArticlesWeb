using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.DbEntities;

namespace ArticlesWeb.MVC.DataAccess.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostWithUser(Guid postId);
        Task<List<Post>> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null);
        Task IncrementCommentCount(Guid postId);
        Task DecrementCommentCount(Guid postId);
    }
}
