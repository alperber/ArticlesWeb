using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesWeb.Business.Results;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Models.ResponseModels;

namespace ArticlesWeb.MVC.Services.Abstract
{
    public interface IPostService
    {
        Task<IResult> AddPost(PostAddModel post);
        Task<IDataResult<List<Post>>> GetAllPosts();
        Task<IDataResult<List<Post>>> GetPostsByUserId(Guid userId);
        Task<IDataResult<List<Post>>> GetPostsByUsername(string username);
        Task<IDataResult<PostDetailedViewModel>> GetPostById(Guid postId);
        Task<IResult> IncrementCommentCount(Guid postId);
        Task<IResult> DeletePostById(Guid postId);
        Task<IDataResult<List<PostSimpleViewModel>>> GetAllPostsWithUser();
        Task<IDataResult<Post>> GetPostWithUser(Guid postId);
        Task<IResult> DeleteUserOwnedPosts(Guid userId);
        Task<IResult> DecrementCommentCount(Guid postId);
    }
}
