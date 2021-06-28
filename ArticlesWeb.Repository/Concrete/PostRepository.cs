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
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ArticlesContext context) : base(context)
        {
        }


        public Post GetPostWithUser(string postId)
        {
            return _context.Posts.AsNoTracking().FirstOrDefault(p => p.PostId == postId);
        }

        public List<Post> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null)
        {
            return filterExpression == null ?
                _context.Posts.Include(p => p.User).ToList():
                _context.Posts.Where(filterExpression).Include(p => p.User).ToList();
        }

        public void IncrementCommentCount(string postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            post.CommentCount += 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;

            _context.SaveChanges();
        }

        public void DecrementCommentCount(string postId)
        {
            var post = _context.Posts.FirstOrDefault(post => post.PostId == postId);
            post.CommentCount -= 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;

            _context.SaveChanges();
        }
    }
}
