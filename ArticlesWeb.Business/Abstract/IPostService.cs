using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;

namespace ArticlesWeb.Business.Abstract
{
    public interface IPostService
    {
        IDataResult<List<Post>> GetPosts(Expression<Func<Post, bool>> filterExpression = null);
        IDataResult<Post> GetPost(int postId);
        IResult IncrementCommentCount(int postId);
    }
}
