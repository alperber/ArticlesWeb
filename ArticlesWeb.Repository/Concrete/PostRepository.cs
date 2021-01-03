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


        public Post GetPostWithUser(int postId)
        {
            return _context.Posts.AsNoTracking().FirstOrDefault(p => p.PostId == postId);
        }

        public List<Post> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null)
        {
            if( filterExpression == null) return _context.Posts.AsNoTracking().Include(p => p.User).AsNoTracking().ToList();
            return _context.Posts.Where(filterExpression).AsNoTracking().Include(p => p.User).AsNoTracking().ToList();
        }

        public void IncrementCommentCount(int postId)
        {
            var post = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == postId);
            post.CommentCount += 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;

            _context.SaveChanges();
        }

        public void DecrementCommentCount(int postId)
        {
            var post = _context.Posts.AsNoTracking().FirstOrDefault(post => post.PostId == postId);
            post.CommentCount -= 1;
            _context.Attach(post);
            _context.Entry(post).Property(x => x.CommentCount).IsModified = true;

            _context.SaveChanges();
        }
    }
}
