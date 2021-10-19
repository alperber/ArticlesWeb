using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticlesWeb.MVC.DataAccess.Abstract;
using ArticlesWeb.MVC.Models.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.MVC.DataAccess.Concrete
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ArticlesContext context) : base(context)
        {
        }

        public async Task<Post> GetPostWithUser(Guid postId)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }
        
        public async Task<List<Post>> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null)
        {
            return filterExpression == null ?
                await _context.Posts.Include(p => p.User).ToListAsync():
                await _context.Posts.Where(filterExpression).Include(p => p.User).ToListAsync();
        }
        
        public async Task IncrementCommentCount(Guid postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            post!.CommentCount += 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;
            
            await _context.SaveChangesAsync();
        }
        
        public async Task DecrementCommentCount(Guid postId)
        {
            var post = _context.Posts.FirstOrDefault(post => post.Id == postId);
            post!.CommentCount -= 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;
            
            await _context.SaveChangesAsync();
        }
    }
}
