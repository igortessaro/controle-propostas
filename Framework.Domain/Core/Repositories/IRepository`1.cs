using Framework.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain.Core.Repositories
{
    public interface IRepository<TEntity> : IRepository
        where TEntity : IBaseEntity
    {
        void Delete(TEntity entity);

        void Dispose();

        TEntity Get(params object[] keys);

        void Insert(TEntity entity);

        List<TEntity> QueryAll(int limitCount = 100);

        void Update(TEntity entity);
    }
}
