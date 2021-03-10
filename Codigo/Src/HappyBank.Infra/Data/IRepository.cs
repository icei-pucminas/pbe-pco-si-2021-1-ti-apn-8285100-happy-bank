using System;
using System.Collections.Generic;

namespace HappyBank.Infra.Data
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(int id);
        List<TEntity> GetAll();
        Guid Add(TEntity entity);
        bool Update(TEntity client);
        bool Delete(TEntity client);
    }
}