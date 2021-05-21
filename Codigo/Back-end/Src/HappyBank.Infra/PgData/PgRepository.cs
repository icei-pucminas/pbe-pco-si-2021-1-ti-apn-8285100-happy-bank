using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;

namespace HappyBank.Infra.Data.Pg
{
    public abstract class PgRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private NpgsqlConnection _connection;
        protected NpgsqlConnection Connection {
            get{
                if(null != this._connection && ConnectionState.Closed.Equals(_connection.State))
                {
                    _connection.Open();
                }                
                return _connection;
            }
        }
        public PgRepository(NpgsqlConnection connection)
        {
            this._connection = connection;
        }

        public abstract Guid Add(TEntity entity);
        public abstract bool Delete(TEntity client);
        public abstract TEntity FindOne(Guid id);
        public abstract List<TEntity> FindAll();
        public abstract bool Update(TEntity client);
    }
}