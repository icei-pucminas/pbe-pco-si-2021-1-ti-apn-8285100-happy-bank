using System;
using System.Collections.Generic;
using HappyBank.Infra.Data;

namespace HappyBank.Infra.DummyData
{
    public abstract class DummyRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        protected readonly List<TEntity> _list = new List<TEntity>();
        public Guid Add(TEntity entity)
        {
            _list.Add(entity);
            return entity.Id;
        }

        public bool Delete(TEntity client)
        {
            return _list.Remove(this.FindOne(client.Id));
        }

        public List<TEntity> FindAll()
        {
            return new List<TEntity>(_list);
        }

        public TEntity FindOne(Guid id)
        {
            return _list.Find(e => e.Id == id);
        }

        public bool Update(TEntity client)
        {
            var itemIndex = _list.FindIndex(e => e.Id == client.Id);

            if(itemIndex < 0)
                return false;

            _list[itemIndex] = client;
            return true;
        }
    }
}