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
        IDataResult<List<Comment>> GetCommentsByPostId(int postId);
        public IResult AddComment(CommentAddModel comment);
        IResult DeleteComment(int commentId);
        IResult DeleteCommentsOnPost(int postId);
        IResult DeleteUserComments(int userId);
    }
}
