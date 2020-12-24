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
    }
}
