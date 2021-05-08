using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.Data.Pg;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class CustomerRepository : PgRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {
            this.Connection.Open();
        }

        /* TODO: Implementar os campos restantes de customer */
        public override Guid Add(Customer entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"customer\" (name, gov_number, street) VALUES (@name, @gov_numer, @street)", Connection))
            {
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("gov_number", entity.Name);
                cmd.Parameters.AddWithValue("street", entity.Street);
                //cmd.Parameters.AddWithValue("username", entity.Username);

                cmd.ExecuteNonQuery();
            }

            return entity.Id;
        }

        public override bool Delete(Customer client)
        {
            using ( var cmd = new NpgsqlCommand("DELETE FROM \"customer\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
            }

            return true;
        }

        public override List<Customer> FindAll()
        {
            throw new NotImplementedException();
        }

        public override Customer FindOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public Customer FindOneByEmail(string username)
        {
            return null;
        }

        public override bool Update(Customer client)
        {
            throw new NotImplementedException();
        }
    }
}