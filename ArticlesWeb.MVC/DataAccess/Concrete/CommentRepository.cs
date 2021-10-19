using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.DataAccess.Abstract;

namespace ArticlesWeb.MVC.DataAccess.Concrete
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ArticlesContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsWithUser(Expression<Func<Comment, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? await _context.Comments.Include(c => c.User).ToListAsync()
                : await _context.Comments.Where(filterExpression).Include(c => c.User).ToListAsync();
        }

        public async Task DeleteUserComments(Guid userId)
        {
            var comments = await _context.Comments.Where(c => c.Id == userId).ToListAsync();
            _context.Comments.RemoveRange(comments);
            await _context.SaveChangesAsync();
        }
    }
}
