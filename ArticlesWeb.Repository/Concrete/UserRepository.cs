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

        public bool IncrementPostCount(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return false;

            user.PostCount += 1;
            _context.SaveChanges();
            return true;
        }

        public bool DecrementPostCount(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return false;

            user.PostCount -= 1;
            _context.SaveChanges();
            return true;
        }

        public bool MakeAdmin(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user.isAdmin)
                return false;

            _context.SaveChanges();
            return true;
        }
    }
}
