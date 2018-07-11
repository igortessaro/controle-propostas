using Framework.Domain.Core.Constants;
using Framework.Domain.Core.Entity;
using Framework.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Infrastructure.Repositories.Relational
{
    public class RelationalRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public RelationalRepository(PrincipalDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public TEntity Get(params object[] keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            return this.dbSet.Find(keys);
        }

        public List<TEntity> QueryAll(int limitCount = RepositoryConstants.DefaultQueryLimitCount)
        {
            return this.Query().Take(limitCount).ToList();
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.dbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.dbSet.Update(entity);
            this.dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.dbSet.Remove(entity);
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected IQueryable<TEntity> Query()
        {
            return this.dbSet.AsNoTracking();
        }

        protected IQueryable<TEntity> QueryAsTracking()
        {
            return this.dbSet;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || this.dbContext == null)
            {
                return;
            }

            this.dbContext.Dispose();
        }
    }
}
