using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.RequestModels;
using ArticlesWeb.MVC.Models.ResponseModels;
using AutoMapper;

namespace ArticlesWeb.MVC.Services
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PostAddModel, Post>();
            CreateMap<CommentAddModel, Comment>();
            CreateMap<User, SimpleUserModel>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<UserLoginModel, User>();
            CreateMap<Post, PostSimpleViewModel>();
            CreateMap<Post, PostDetailedViewModel>();
            CreateMap<User, UserViewModel>();
        }
    }
}