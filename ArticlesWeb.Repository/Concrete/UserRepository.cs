using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;
using ArticlesWeb.Repository.Abstract;

namespace ArticlesWeb.Repository.Concrete
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(ArticlesContext context) : base(context)
        {
        }

        public void IncrementPostCount(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null) return;

            user.PostCount += 1;
            _context.SaveChanges();
        }

        public void DecrementPostCount(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null) return;

            user.PostCount -= 1;
            _context.SaveChanges();
        }

        public void UpdateRoleAsAdmin(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            
            if (user is null || user.IsAdmin) return;

            user.IsAdmin = true;
           _context.SaveChanges();
        }
    }
}
