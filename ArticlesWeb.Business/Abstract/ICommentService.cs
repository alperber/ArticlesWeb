using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Abstract
{
    public interface ICommentService
    {
        IDataResult<List<Comment>> GetCommentsByPostId(string postId);
        public IResult AddComment(CommentAddModel comment);
        IResult DeleteComment(string commentId);
        IResult DeleteCommentsOnPost(string postId);
        IResult DeleteUserComments(string userId);
    }
}
