using System;
using System.Collections.Generic;
using Npgsql;
using HappyBank.Infra.Data;

namespace HappyBank.Infra.PgData
{
    public abstract class PgCrudRepository<TEntity> : PgRepository, IRepository<TEntity> where TEntity : IEntity
    {
        public PgCrudRepository(NpgsqlConnection connection) : base(connection)
        {
            
        }

        public abstract Guid Add(TEntity entity);
        public abstract bool Delete(TEntity client);
        public abstract TEntity FindOne(Guid id);
        public abstract List<TEntity> FindAll();
        public abstract bool Update(TEntity client);
    }
}