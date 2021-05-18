using System;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Model
{
    public abstract class Entity : IEntity
    {
        public Guid Id {get; protected set;}       
    }
}