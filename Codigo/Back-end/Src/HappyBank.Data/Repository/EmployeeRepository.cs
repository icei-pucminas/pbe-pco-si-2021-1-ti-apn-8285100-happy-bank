using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.Data.Pg;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class EmployeeRepository : PgRepository<Employee>, IEmployeeRepository
    {
        private readonly IBankRepository _bankRepository;
        public EmployeeRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {
            this._bankRepository = new BankRepository(connection);
        }

        public override Guid Add(Employee entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"employee\" (name, bank_id, registration, wage, \"function\") VALUES (@name, @bank_id, @registration, @wage, @function) RETURNING id", Connection))
            {
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("bank_id", entity.BankId);
                cmd.Parameters.AddWithValue("registration", entity.Registration);                
                cmd.Parameters.AddWithValue("wage", entity.Wage);                
                cmd.Parameters.AddWithValue("function", entity.Function);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Employee client)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM \"employee\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Prepare();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override List<Employee> FindAll()
        {
            var result = new List<Employee>();
            using (var cmd = new NpgsqlCommand("SELECT id, name, bank_id, registration, wage, \"function\" FROM employee order by id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetEmployee(reader));
                        }
                    }
                }
            }

            //result.ForEach(e => e.Bank = )

            return result;
        }

        public override Employee FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, name, bank_id, registration, wage, \"function\" FROM employee WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetEmployee(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Employee FindOneByRegistration(string registration)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, name, bank_id, registration, wage, \"function\" FROM employee WHERE registration = @registration", Connection))
            {
                cmd.Parameters.AddWithValue("registration", registration);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetEmployee(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Employee entity)
        {
            using (var cmd = new NpgsqlCommand("UPDATE \"employee\" SET registration=@registration, bank_id=@bank_id, wage=@wage, name=@name, \"function\"=@function WHERE id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("registration", entity.Registration);
                cmd.Parameters.AddWithValue("bank_id", entity.BankId);
                cmd.Parameters.AddWithValue("wage", entity.Wage);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("function", entity.Function);
                cmd.Prepare();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Employee GetEmployee(NpgsqlDataReader reader)
        {
            return new Employee(
                reader.GetGuid(0), 
                reader.GetString(1), 
                reader.GetGuid(2),
                reader.GetString(3), 
                reader.GetDecimal(4), 
                reader.GetString(5)
            );
        }
    }
}