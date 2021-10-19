using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.MVC.DataAccess.Abstract;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Models.ResponseModels;
using ArticlesWeb.MVC.Services.Abstract;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesWeb.MVC.Services.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public PostService(
            IPostRepository repository, 
            IServiceProvider serviceProvider,
            IMapper mapper)
        {
            _repository = repository;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public async Task<IResult> AddPost(PostAddModel post)
        {
            Post newPost = _mapper.Map<Post>(post);

            try
            {
                await _repository.Add(newPost);
                return new SuccessResult(Messages.PostCreated);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public async Task<IDataResult<List<Post>>> GetAllPosts()
        {
            return new SuccessDataResult<List<Post>>(await _repository.GetList());
        }

        public async Task<IDataResult<List<Post>>> GetPostsByUserId(Guid userId)
        {
            return new SuccessDataResult<List<Post>>(await _repository.GetPostsWithUser(p => p.UserId == userId));
        }

        public async Task<IDataResult<List<Post>>> GetPostsByUsername(string username)
        {
            return new SuccessDataResult<List<Post>>(
                await _repository.GetPostsWithUser(p => p.User.UserName == username));
        }

        public async Task<IDataResult<PostDetailedViewModel>> GetPostById(Guid postId)
        {
            var post = await _repository.GetPostWithUser(postId);
            var postDto = _mapper.Map<PostDetailedViewModel>(post);
            
            return new SuccessDataResult<PostDetailedViewModel>(postDto);
        }

        public async Task<IResult> IncrementCommentCount(Guid postId)
        {
            await _repository.IncrementCommentCount(postId);
            return new SuccessResult();
        }

        public async Task<IResult> DeletePostById(Guid postId)
        {
            try
            {
                var post = await _repository.Get(p => p.Id == postId);

                if (post == null)
                {
                    return new ErrorResult(Messages.PostDoesntExists);
                }

                var commentService = _serviceProvider.GetRequiredService<ICommentService>();
                await commentService.DeleteCommentsOnPost(postId);
                await _repository.Delete(post);
                return new SuccessResult(Messages.PostDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public async Task<IDataResult<List<PostSimpleViewModel>>> GetAllPostsWithUser()
        {
            var posts = await _repository.GetPostsWithUser();

            var postDtos = _mapper.Map<List<PostSimpleViewModel>>(posts);
            
            return new SuccessDataResult<List<PostSimpleViewModel>>(postDtos);
        }

        public async Task<IDataResult<Post>> GetPostWithUser(Guid postId)
        {
            var data = await _repository.GetPostWithUser(postId);
            return new SuccessDataResult<Post>(data);
        }

        public async Task<IResult> DeleteUserOwnedPosts(Guid userId)
        {
            var posts = await _repository.GetList(p => p.UserId == userId);
            if (posts.Count == 0)
            {
                return new ErrorResult(Messages.UserDoesntHavePost);
            }

            foreach (var post in posts)
            {
                await DeletePostById(post.Id);
            }

            return new SuccessResult();
        }

        public async Task<IResult> DecrementCommentCount(Guid postId)
        {
            await _repository.DecrementCommentCount(postId);
            return new SuccessResult();
        }
    }
}
