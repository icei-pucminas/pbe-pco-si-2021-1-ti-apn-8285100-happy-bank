using HappyBank.Infra.Data.Pg;
using System;
using System.Collections.Generic;
using Npgsql;

namespace HappyBank.IntegrationTests.PgData
{
    public class SampleRepository : PgRepository<SampleEntity>
    {
        public SampleRepository(NpgsqlConnection connection) : base(connection) { }

        public override Guid Add(SampleEntity entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO sample (id, title) VALUES (@id, @title)", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("title", entity.Title);

                cmd.ExecuteNonQuery();
            }

            return entity.Id;
        }

        public override bool Delete(SampleEntity client)
        {
            return false;
        }

        public override SampleEntity FindOne(Guid id)
        {
            return null;
        }

        public override List<SampleEntity> FindAll()
        {
            return null;
        }

        public override bool Update(SampleEntity client)
        {
            return false;
        }
    }
}