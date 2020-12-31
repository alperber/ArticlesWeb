using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ArticlesWeb.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
            // Neden AsNoTracking ?
            // Veritabanından alınan bir nesne'ye sahipken aynı zamanda üzerinde
            // güncelleme işlemi yapabilmek için
            return _context.Set<TEntity>().Where(filterExpression).AsNoTracking().FirstOrDefault();
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filterExpression = null)
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

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
