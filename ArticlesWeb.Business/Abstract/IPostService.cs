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
        IDataResult<List<Post>> GetPostsByUserId(int userId);
        IDataResult<List<Post>> GetPostsByUsername(string username);
        IDataResult<Post> GetPostById(int postId);
        IResult IncrementCommentCount(int postId);
        IResult DeletePostById(int postId);
        IDataResult<List<Post>> GetAllPostsWithUser();
        IDataResult<Post> GetPostWithUser(int postId);
        IResult DeletePost(int postId);
        IResult DeleteUserOwnedPosts(int userId);
    }
}
