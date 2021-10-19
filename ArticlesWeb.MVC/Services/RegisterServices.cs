using ArticlesWeb.MVC.Services.Abstract;
using ArticlesWeb.MVC.Services.Concrete;
using ArticlesWeb.MVC.Services.Validators.User;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesWeb.MVC.Services
{
    public static class RegisterServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddAutoMapper(typeof(MapperConfig));
            
            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<UserLoginValidator>();
                    fv.DisableDataAnnotationsValidation = true;
                });
            
            return services;
        }
    }
}