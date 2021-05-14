using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.Data.Pg;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class EmployeesRepository : PgRepository<Employees>, IEmployeesRepository
    {
        public EmployeesRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {
            
        }

        public override Guid Add(Employees entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"employees\" (registration, bank_id, wage, name, function) VALUES (@registration, @bank_id, @wage, @name, @function) RETURNING id", Connection))
            {
                cmd.Parameters.AddWithValue("registration", entity.Registration);
                cmd.Parameters.AddWithValue("bank_id", entity.Bank);
                cmd.Parameters.AddWithValue("wage", entity.Wage);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("function", entity.Function);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Employees client)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM \"employees\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Prepare();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override List<Employees> FindAll()
        {
            var result = new List<Employees>();
            using (var cmd = new NpgsqlCommand("SELECT id, registration, bank_id, wage, name, function FROM employees order by id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetEmployees(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Employees FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, registration, bank_id, wage, name, function FROM employees WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetEmployees(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Employees FindOneByRegistration (string registration)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, registration, bank_id, wage, name, function FROM employees WHERE registration = @registration", Connection))
            {
                cmd.Parameters.AddWithValue("registration", registration);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetEmployees(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Employees entity)
        {
            using (var cmd = new NpgsqlCommand("UPDATE \"employees\" SET registration=@registration, bank_id=@bank_id, wage=@wage, name=@name, function=@function WHERE id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("registration", entity.Registration);
                cmd.Parameters.AddWithValue("bank_id", entity.Bank);
                cmd.Parameters.AddWithValue("wage", entity.Wage);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("function", entity.Function);
                cmd.Prepare();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Employees GetEmployees(NpgsqlDataReader reader)
        {
            return new Employees(
                reader.GetGuid(0), 
                reader.GetString(1), 
                reader.GetBank(2), 
                reader.GetDecimal(3), 
                reader.GetString(4), 
                reader.GetString(5)
            );
        }
    }
}