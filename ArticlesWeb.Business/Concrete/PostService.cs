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
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesWeb.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public PostService(IPostRepository repository, IUserService userService, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _userService = userService;
            _serviceProvider = serviceProvider;
        }

        public IResult AddPost(PostAddModel post)
        {
            Post newPost = PostHelper.CreatePost(post);

            try
            {
                _repository.Add(newPost);
                var response = _userService.IncrementPostCount(post.UserId ?? 0);

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
            try
            {
                var post = _repository.Get(p => p.PostId == postId);

                if (post == null)
                {
                    return new ErrorResult(Messages.PostDoesntExists);
                }

                var commentService = _serviceProvider.GetRequiredService<ICommentService>();
                commentService.DeleteCommentsOnPost(postId);
                _userService.DecrementPostCount(post.UserId ?? 0);
                _repository.Delete(post);
                return new SuccessResult(Messages.PostDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<List<Post>> GetAllPostsWithUser()
        {
            return new SuccessDataResult<List<Post>>(_repository.GetPostsWithUser());
        }

        public IDataResult<Post> GetPostWithUser(int postId)
        {
            var data = _repository.GetPostWithUser(postId);
            return new SuccessDataResult<Post>(data);
        }

        public IResult DeleteUserOwnedPosts(int userId)
        {
            var posts = _repository.GetList(p => p.UserId == userId);
            if (posts.Count == 0)
            {
                return new ErrorResult(Messages.UserDoesntHavePost);
            }

            foreach (var post in posts)
            {
                DeletePostById(post.PostId);
            }

            return new SuccessResult();
        }

        public IResult DecrementCommentCount(int postId)
        {
            _repository.DecrementCommentCount(postId);
            return new SuccessResult();
        }
    }
}
