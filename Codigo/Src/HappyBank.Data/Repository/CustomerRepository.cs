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

        public override Guid Add(Customer entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"user\" (id, name, username) VALUES (@id, @name, @username)", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("username", entity.Username);

                cmd.ExecuteNonQuery();
            }

            return entity.Id;
        }

        public override bool Delete(User client)
        {
            using ( var cmd = new NpgsqlCommand("DELETE FROM \"user\" (id, name, username) WHERE (@id, @name, @username)", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Parameters.AddWithValue("name", client.Name);
                cmd.Parameters.AddWithValue("username", client.Username);
            }

            return true;
        }

        public override List<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public override User FindOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public User FindOneByUsername(string username)
        {
            return null;
        }

        public override bool Update(User client)
        {
            throw new NotImplementedException();
        }
    }
}