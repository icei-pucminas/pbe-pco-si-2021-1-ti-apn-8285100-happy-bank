using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class AccountRepository : PgCrudRepository<Account>, IAccountRepository
    {
        private readonly IBankRepository _bankRepository;
        private readonly ICustomerRepository _customerRepository;
        public AccountRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {
            this._bankRepository = new BankRepository(connection);
            this._customerRepository = new CustomerRepository(connection);
        }

        public override Guid Add(Account entity)
        {
            if (entity.BankId == Guid.Empty || entity.CustomerId == Guid.Empty)
            {
                throw new InvalidOperationException();
            }

            using (var cmd = new NpgsqlCommand("INSERT INTO \"account\" (bank_id, customer_id, agency_number) VALUES (@bank_id, @customer_id, @agency_number) RETURNING id", Connection))
            {
                cmd.Parameters.AddWithValue("bank_id", entity.BankId);
                cmd.Parameters.AddWithValue("customer_id", entity.CustomerId);
                cmd.Parameters.AddWithValue("agency_number", entity.AgencyNumber);

                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Account client)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM \"account\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Prepare();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override List<Account> FindAll()
        {
            var result = new List<Account>();
            using (var cmd = new NpgsqlCommand("SELECT id, bank_id, customer_id, agency_number, account_number FROM account order by id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetAccount(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Account FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, bank_id, customer_id, agency_number, account_number FROM account WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetAccount(reader);
                        }
                    }
                }
            }

            return null;
        }

        public List<Account> FindByCustomerId(Guid customerId)
        {
            var result = new List<Account>();
            using (var cmd = new NpgsqlCommand("SELECT id, bank_id, customer_id, agency_number, account_number FROM account WHERE customer_id = @customer_id", Connection))
            {
                cmd.Parameters.AddWithValue("customer_id", customerId);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetAccount(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override bool Update(Account entity)
        {
            using (var cmd = new NpgsqlCommand("UPDATE \"account\" SET bank_id=@bank_id, customer_id=@customer_id, agency_number=@agency_number, account_number=@account_number WHERE id=@id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("bank_id", entity.BankId);
                cmd.Parameters.AddWithValue("customer_id", entity.CustomerId);
                cmd.Parameters.AddWithValue("agency_number", entity.AgencyNumber);
                cmd.Parameters.AddWithValue("account_number", entity.AccountNumber);
                cmd.Prepare();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Account GetAccount(NpgsqlDataReader reader)
        {
            return new Account(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetGuid(2),
                reader.GetInt32(3),
                reader.GetInt32(4)
            );
        }

        public Account FindOneByAgencyAndAccountNumber(int agencyNumber, int accountNumber)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, bank_id, customer_id, agency_number, account_number FROM account WHERE agency_number = @agency_number and account_number = @account_number and bank_id = @bank_id", Connection))
            {
                cmd.Parameters.AddWithValue("agency_number", agencyNumber);
                cmd.Parameters.AddWithValue("agency_number", agencyNumber);
                cmd.Parameters.AddWithValue("account_number", accountNumber);
                cmd.Parameters.AddWithValue("bank_id", _bankRepository.HappyBank.Id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetAccount(reader);
                        }
                    }
                }
            }

            return null;
        }

        public Account FindOneByAgencyAndAccountNumber(Guid bankId, int agencyNumber, int accountNumber)
        {
             using (var cmd = new NpgsqlCommand("SELECT id, bank_id, customer_id, agency_number, account_number FROM account WHERE agency_number = @agency_number and account_number = @account_number and bank_id = @bank_id", Connection))
            {
                cmd.Parameters.AddWithValue("agency_number", agencyNumber);
                cmd.Parameters.AddWithValue("agency_number", agencyNumber);
                cmd.Parameters.AddWithValue("account_number", accountNumber);
                cmd.Parameters.AddWithValue("bank_id", bankId);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetAccount(reader);
                        }
                    }
                }
            }

            return null;
        }
    }
}