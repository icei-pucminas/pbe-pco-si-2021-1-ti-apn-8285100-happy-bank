using System;
using System.Collections.Generic;
using Npgsql;

namespace HappyBank.Infra.Data.Pg
{
    public abstract class PgRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected NpgsqlConnection Connection {get;}
        public PgRepository(NpgsqlConnection connection)
        {
            this.Connection = connection;
        }

        public abstract Guid Add(TEntity entity);
        public abstract bool Delete(TEntity client);
        public abstract TEntity FindOne(Guid id);
        public abstract List<TEntity> FindAll();
        public abstract bool Update(TEntity client);
    }
}