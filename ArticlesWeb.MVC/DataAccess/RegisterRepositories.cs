using ArticlesWeb.MVC.DataAccess.Abstract;
using ArticlesWeb.MVC.DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesWeb.MVC.DataAccess
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            return services;
        }
    }
}