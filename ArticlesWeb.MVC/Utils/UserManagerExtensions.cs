using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesWeb.MVC.Models.DbEntities;
using ArticlesWeb.MVC.Models.ResponseModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWeb.MVC.Utils
{
    public static class UserManagerExtensions
    {
        public static async Task<UserViewModel> GetUserDetailsById(this UserManager<User> manager, Guid userId, IMapper mapper)
        {
            var user = await manager.Users.FirstOrDefaultAsync(user => user.Id == userId);
            
            // map user to view user
            return user is not null ? mapper.Map<UserViewModel>(user) : null;
        }
        
        public static async Task<IList<User>> GetAllUsers(this UserManager<User> manager)
        {
            return await manager.Users.ToListAsync();
        }
    }
}