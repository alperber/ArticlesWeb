using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticlesWeb.MVC.DataAccess.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task Add(TEntity entity);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filterExpression);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filterExpression = null);
        Task Delete(TEntity entity);
        Task DeleteRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
    }
}
