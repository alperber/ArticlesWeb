﻿using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Business.Abstract;
using ArticlesWeb.Business.Helpers;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;

namespace ArticlesWeb.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IPostService _postService;

        public CommentService(ICommentRepository repository, IPostService postService)
        {
            _repository = repository;
            _postService = postService;
        }

        public IDataResult<List<Comment>> GetCommentsByPostId(int postId)
        {
            return new SuccessDataResult<List<Comment>>(_repository.GetCommentsWithUser(c => c.PostId == postId));
        }

        public IResult AddComment(CommentAddModel comment)
        {
            Comment newComment = CommentHelper.CreateComment(comment);

            _postService.IncrementCommentCount(newComment.PostId ?? 0);
            _repository.Add(newComment);
            return new SuccessResult();
        }

        public IResult DeleteComment(int commentId)
        {
            var comment = _repository.Get(c => c.CommentId == commentId);
            if (comment == null) return new ErrorResult(Messages.CommentDoesntExists);

            _postService.DecrementCommentCount(comment.PostId ?? 0);
            _repository.Delete(comment);

            return new SuccessResult();
        }

        public IResult DeleteCommentsOnPost(int postId)
        {
            var comments = _repository.GetList(c => c.PostId == postId);

            if (comments.Count == 0)
                return new SuccessResult();

            try
            {
                foreach (var comment in comments)
                {
                    DeleteComment(comment.CommentId);
                }
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}
