using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Repository.Abstract;

namespace ArticlesWeb.Repository.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class, new()
    {
        protected readonly ArticlesContext _context;
        public Repository(ArticlesContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _context.Set<TEntity>().Where(filterExpression).FirstOrDefault();
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? _context.Set<TEntity>().ToList()
                : _context.Set<TEntity>().Where(filterExpression).ToList();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
    }
}
