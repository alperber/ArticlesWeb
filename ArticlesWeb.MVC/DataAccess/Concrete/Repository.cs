using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Threading.Tasks;
using ArticlesWeb.MVC.DataAccess.Abstract;

namespace ArticlesWeb.MVC.DataAccess.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class, new()
    {
        protected readonly ArticlesContext _context;
        public Repository(ArticlesContext context)
        {
            _context = context;
        }
        public async Task Add(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await _context.Set<TEntity>().Where(filterExpression).FirstOrDefaultAsync();
        }
        
        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            return filterExpression == null
                ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().Where(filterExpression).AsNoTracking().ToListAsync();
        }
        
        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
        
        public async Task Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
