using HappyBank.Infra.Data;
using System;

namespace HappyBank.IntegrationTests.PgData
{
    public class SampleEntity : IEntity
    {
        public SampleEntity(Guid id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public Guid Id {get; protected set;}
        public string Title {get; protected set;}
    }
}