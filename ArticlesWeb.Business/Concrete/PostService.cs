using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Repository.Abstract;

namespace ArticlesWeb.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public IDataResult<List<Post>> GetPosts(Expression<Func<Post, bool>> filterExpression = null)
        {
            return new SuccessDataResult<List<Post>>(_repository.GetPostsWithUser(filterExpression));
        }

        public IDataResult<Post> GetPost(int postId)
        {
            return new SuccessDataResult<Post>(_repository.GetPostWithUser(postId));
        }

        public IResult IncrementCommentCount(int postId)
        {
            _repository.IncrementCommentCount(postId);
            return new SuccessResult();
        }
    }
}
