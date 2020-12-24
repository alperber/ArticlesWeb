using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ArticlesWeb.Repository.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        void Add(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> filterExpression);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filterExpression = null);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
