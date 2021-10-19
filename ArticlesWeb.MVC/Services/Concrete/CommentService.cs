using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.MVC.DataAccess.Abstract;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Services.Abstract;
using AutoMapper;

namespace ArticlesWeb.MVC.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IPostService postService, IMapper mapper)
        {
            _repository = repository;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<Comment>>> GetCommentsByPostId(Guid postId)
        {
            return new SuccessDataResult<List<Comment>>(await _repository.GetCommentsWithUser(c => c.PostId == postId));
        }

        public async Task<IResult> AddComment(CommentAddModel comment)
        {
            Comment newComment = _mapper.Map<Comment>(comment);

            await _postService.IncrementCommentCount(newComment.PostId);
            await _repository.Add(newComment);
            return new SuccessResult();
        }

        public async Task<IResult> DeleteComment(Guid commentId)
        {
            var comment = await _repository.Get(c => c.Id == commentId);
            if (comment == null) return new ErrorResult(Messages.CommentDoesntExists);

            await _postService.DecrementCommentCount(comment.PostId);
            await _repository.Delete(comment);

            return new SuccessResult();
        }

        public async Task<IResult> DeleteCommentsOnPost(Guid postId)
        {
            var comments = await _repository.GetList(c => c.PostId == postId);

            if (comments.Count == 0)
                return new SuccessResult();

            try
            {
                foreach (var comment in comments)
                {
                    await DeleteComment(comment.Id);
                }
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public async Task<IResult> DeleteUserComments(Guid userId)
        {
            var comments = await _repository.GetCommentsWithUser(c => c.UserId == userId);
            try
            {
                foreach (var comment in comments)
                {
                    await _postService.DecrementCommentCount(comment.PostId);
                }
                await _repository.DeleteRange(comments);
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}
