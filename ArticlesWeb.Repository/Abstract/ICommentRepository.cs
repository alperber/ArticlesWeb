using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Entities.DbEntities;

namespace ArticlesWeb.Repository.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetCommentsWithUser(Expression<Func<Comment, bool>> filterExpression = null);
    }
}
