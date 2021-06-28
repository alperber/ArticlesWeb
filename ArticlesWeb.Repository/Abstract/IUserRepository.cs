using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Entities.DbEntities;
using ArticlesWeb.Entities.RequestModels;

namespace ArticlesWeb.Repository.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        void IncrementPostCount(string userId);
        void DecrementPostCount(string userId);
        void UpdateRoleAsAdmin(string userId);
    }
}
