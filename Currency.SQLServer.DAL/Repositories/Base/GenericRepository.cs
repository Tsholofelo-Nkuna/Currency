using Currency.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Currency.SQLServer.DAL.Repositories.Base
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        TEntity? Insert(TEntity entity);

        TEntity? Update(TEntity entity);

        TEntity? Delete(TEntity entity);


    }
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> _entities;
        private readonly WebDbContext _webDbContext;
        public GenericRepository(WebDbContext dbContext) { 
            this._webDbContext = dbContext;
            this._entities = this._webDbContext.Set<TEntity>();
        }
        public TEntity? Delete(TEntity entity)
        {
            var toBeDeleted = this._entities.FirstOrDefault(x => x.Id == entity.Id);
            if(toBeDeleted != null)
            {
                this._entities.Remove(toBeDeleted);
            }

            return toBeDeleted;
           
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
           return this._entities.Where(predicate);
        }

        public TEntity? Insert(TEntity entity)
        {
           this._entities.Add(entity);
           return entity;
        }

        public TEntity? Update(TEntity entity)
        {
            var toBeUpdated = this._entities.FirstOrDefault(x => x.Id == entity.Id);
            if(toBeUpdated != null)
            {
                this._entities.Update(entity);
            }

            return toBeUpdated;
        }

        public int SaveChanges()
        {
            return this._webDbContext.SaveChanges();
        }
    }
}
