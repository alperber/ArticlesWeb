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
            return _context.Posts.FirstOrDefault(p => p.PostId == postId);
        }

        public List<Post> GetPostsWithUser(Expression<Func<Post, bool>> filterExpression = null)
        {
            if( filterExpression == null) return _context.Posts.Include(p => p.User).ToList();
            return _context.Posts.Where(filterExpression).Include(p => p.User).ToList();
        }

        public void IncrementCommentCount(int postId)
        {
            _context.Posts.FirstOrDefault(post => post.PostId == postId).CommentCount += 1;
            _context.SaveChanges();
        }

        public void DecrementCommentCount(int postId)
        {
            _context.Posts.FirstOrDefault(post => post.PostId == postId).CommentCount -= 1;
            _context.SaveChanges();
        }
    }
}
