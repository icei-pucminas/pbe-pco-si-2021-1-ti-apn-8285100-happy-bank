using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class CustomerRepository : PgCrudRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {
            
        }

        public override Guid Add(Customer entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"customer\" (name, gov_number, street, district, city, state, address_number, birth_date, phone, email, password) VALUES (@name, @gov_number, @street, @district, @city, @state, @address_number, @birth_date, @phone, @email, @password) RETURNING id", Connection))
            {
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("gov_number", entity.GovNumber);
                cmd.Parameters.AddWithValue("street", entity.Street);
                cmd.Parameters.AddWithValue("district", entity.District);
                cmd.Parameters.AddWithValue("city", entity.City);
                cmd.Parameters.AddWithValue("state", entity.State);
                cmd.Parameters.AddWithValue("address_number", entity.AddressNumber);
                cmd.Parameters.AddWithValue("birth_date", entity.BirthDate);
                cmd.Parameters.AddWithValue("phone", entity.Phone);
                cmd.Parameters.AddWithValue("email", entity.Email);
                cmd.Parameters.AddWithValue("password", entity.Password);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Customer client)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM \"customer\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Prepare();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override List<Customer> FindAll()
        {
            var result = new List<Customer>();
            using (var cmd = new NpgsqlCommand("SELECT id, name, gov_number, street, district, city, state, address_number, birth_date, phone, email, password FROM customer order by id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetCustomer(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Customer FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, name, gov_number, street, district, city, state, address_number, birth_date, phone, email, password FROM customer WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetCustomer(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Customer FindOneByEmail(string email)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, name, gov_number, street, district, city, state, address_number, birth_date, phone, email, password FROM customer WHERE email = @email", Connection))
            {
                cmd.Parameters.AddWithValue("email", email);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetCustomer(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Customer entity)
        {
            using (var cmd = new NpgsqlCommand("UPDATE \"customer\" SET name=@name, gov_number=@gov_number, street=@street, district=@district, city=@city, state=@state, address_number=@address_number, birth_date=@birth_date, phone=@phone, email=@email, password=@password WHERE id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("gov_number", entity.GovNumber);
                cmd.Parameters.AddWithValue("street", entity.Street);
                cmd.Parameters.AddWithValue("district", entity.District);
                cmd.Parameters.AddWithValue("city", entity.City);
                cmd.Parameters.AddWithValue("state", entity.State);
                cmd.Parameters.AddWithValue("address_number", entity.AddressNumber);
                cmd.Parameters.AddWithValue("birth_date", entity.BirthDate);
                cmd.Parameters.AddWithValue("phone", entity.Phone);
                cmd.Parameters.AddWithValue("email", entity.Email);
                cmd.Parameters.AddWithValue("password", entity.Password);
                cmd.Prepare();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Customer GetCustomer(NpgsqlDataReader reader)
        {
            return new Customer(
                reader.GetGuid(0), 
                reader.GetString(1), 
                reader.GetString(2), 
                reader.GetString(3), 
                reader.GetString(4), 
                reader.GetString(5), 
                reader.GetString(6), 
                reader.GetString(7), 
                reader.GetDateTime(8), 
                reader.GetString(9), 
                reader.GetString(10), 
                reader.GetString(11)
            );
        }
    }
}