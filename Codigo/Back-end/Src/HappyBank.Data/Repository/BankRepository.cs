using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class BankRepository : PgCrudRepository<Bank>, IBankRepository
    {
        private Bank _happyBank;
        public int HappyBankNumber => 171;

        public BankRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {

        }

        public Bank HappyBank {
            get{
                if(null == _happyBank)
                {
                    _happyBank = this.FindOneByBankNumber(HappyBankNumber);
                }

                return _happyBank;
            }
        }

        public override Guid Add(Bank entity)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO \"bank\" (bank_number, name, gov_number, street, district, city, state, address_number) VALUES (@bank_number, @name, @gov_number, @street, @district, @city, @state, @address_number) RETURNING id", Connection))
            {
                cmd.Parameters.AddWithValue("bank_number", entity.BankNumber);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("gov_number", entity.GovNumber);
                cmd.Parameters.AddWithValue("street", entity.Street);
                cmd.Parameters.AddWithValue("district", entity.District);
                cmd.Parameters.AddWithValue("city", entity.City);
                cmd.Parameters.AddWithValue("state", entity.State);
                cmd.Parameters.AddWithValue("address_number", entity.AddressNumber);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Bank client)
        {
            using (var cmd = new NpgsqlCommand("DELETE FROM \"bank\" WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", client.Id);
                cmd.Prepare();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override List<Bank> FindAll()
        {
            var result = new List<Bank>();
            using (var cmd = new NpgsqlCommand("SELECT id, bank_number, name, gov_number, street, district, city, state, address_number FROM bank order by id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetBank(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Bank FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, bank_number, name, gov_number, street, district, city, state, address_number FROM bank WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetBank(reader);
                        }
                    }
                }
            }

            return null;

        }

        public Bank FindOneByBankNumber(int bankNumber)
        {
            using (var cmd = new NpgsqlCommand("SELECT id, bank_number, name, gov_number, street, district, city, state, address_number FROM bank WHERE  bank_number = @bank_number", Connection))
            {
                cmd.Parameters.AddWithValue("bank_number", bankNumber);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetBank(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Bank entity)
        {
            using (var cmd = new NpgsqlCommand("UPDATE \"bank\" SET bank_number = @bank_number, name=@name, gov_number=@gov_number, street=@street, district=@district, city=@city, state=@state, address_number=@address_number WHERE id = @id", Connection))
            {
                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("bank_number", entity.BankNumber);
                cmd.Parameters.AddWithValue("name", entity.Name);
                cmd.Parameters.AddWithValue("gov_number", entity.GovNumber);
                cmd.Parameters.AddWithValue("street", entity.Street);
                cmd.Parameters.AddWithValue("district", entity.District);
                cmd.Parameters.AddWithValue("city", entity.City);
                cmd.Parameters.AddWithValue("state", entity.State);
                cmd.Parameters.AddWithValue("address_number", entity.AddressNumber);
                cmd.Prepare();

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Bank GetBank(NpgsqlDataReader reader)
        {
            return new Bank(
                reader.GetGuid(0),
                reader.GetInt32(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetString(7),
                reader.GetString(8)
            );
        }
    }
}