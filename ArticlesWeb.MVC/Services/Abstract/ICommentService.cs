using System;
using System.Collections.Generic;
using System.Text;
using ArticlesWeb.Business.Results;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;

namespace ArticlesWeb.MVC.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<List<Comment>>> GetCommentsByPostId(Guid postId);
        Task<IResult> AddComment(CommentAddModel comment);
        Task<IResult> DeleteComment(Guid commentId);
        Task<IResult> DeleteCommentsOnPost(Guid postId);
        Task<IResult> DeleteUserComments(Guid userId);
    }
}
