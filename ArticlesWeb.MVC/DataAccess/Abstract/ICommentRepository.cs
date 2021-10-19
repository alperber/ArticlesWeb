using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.DbEntities;

namespace ArticlesWeb.MVC.DataAccess.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsWithUser(Expression<Func<Comment, bool>> filterExpression = null);
        Task DeleteUserComments(Guid userId);
    }
}
