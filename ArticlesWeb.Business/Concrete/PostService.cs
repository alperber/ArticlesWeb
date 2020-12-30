using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Helpers;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;

namespace ArticlesWeb.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserService _userService;

        public PostService(IPostRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public IResult AddPost(PostAddModel post)
        {
            Post newPost = PostHelper.CreatePost(post);

            try
            {
                _repository.Add(newPost);
                var response = _userService.IncrementPostCount(post.UserId ?? 1);

                if(response.Success)
                    return new SuccessResult(Messages.PostCreated);

                return new ErrorResult(response.Message);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<List<Post>> GetAllPosts()
        {
            return new SuccessDataResult<List<Post>>(_repository.GetList());
        }

        public IDataResult<List<Post>> GetPostsByUserId(int userId)
        {
            return new SuccessDataResult<List<Post>>(_repository.GetPostsWithUser(p => p.UserId == userId));
        }

        public IDataResult<List<Post>> GetPostsByUsername(string username)
        {
            return new SuccessDataResult<List<Post>>(
                _repository.GetPostsWithUser(p => p.User.Username == username));
        }

        public IDataResult<Post> GetPostById(int postId)
        {
            return new SuccessDataResult<Post>(_repository.GetPostWithUser(postId));
        }

        public IResult IncrementCommentCount(int postId)
        {
            _repository.IncrementCommentCount(postId);
            return new SuccessResult();
        }

        public IResult DeletePostById(int postId)
        {
            var post = _repository.Get(p => p.PostId == postId);

            if (post == null)
            {
                return new ErrorResult(Messages.PostDoesntExists);
            }
            _repository.Delete(post);
            return new SuccessResult(Messages.PostDeleted);
        }

        public IDataResult<List<Post>> GetAllPostsWithUser()
        {
            return new SuccessDataResult<List<Post>>(_repository.GetPostsWithUser());
        }

        public IDataResult<Post> GetPostWithUser(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
