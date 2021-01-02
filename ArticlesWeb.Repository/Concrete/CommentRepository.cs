using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.Repository.Concrete
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ArticlesContext context) : base(context)
        {
        }

        public List<Comment> GetCommentsWithUser(Expression<Func<Comment, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? _context.Comments.Include(c => c.User).ToList()
                : _context.Comments.Where(filterExpression).Include(c => c.User).ToList();
        }
    }
}
