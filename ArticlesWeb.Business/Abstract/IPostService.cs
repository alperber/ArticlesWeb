using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Business.Results;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Business.Abstract
{
    public interface IPostService
    {
        IResult AddPost(PostAddModel post);
        IDataResult<List<Post>> GetAllPosts();
        IDataResult<List<Post>> GetPostsByUserId(string userId);
        IDataResult<List<Post>> GetPostsByUsername(string username);
        IDataResult<Post> GetPostById(string postId);
        IResult IncrementCommentCount(string postId);
        IResult DeletePostById(string postId);
        IDataResult<List<Post>> GetAllPostsWithUser();
        IDataResult<Post> GetPostWithUser(string postId);
        IResult DeleteUserOwnedPosts(string userId);
        IResult DecrementCommentCount(string postId);
    }
}
