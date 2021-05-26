using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class DepositRepository : PgCrudRepository<Deposit>, IDepositRepository
    {
        private const string INSERT_QUERY = @"DO $$
DECLARE
   t_id uuid;
begin
   INSERT INTO transaction(account_id, kind, value, execution_date) VALUES (@account_id, @kind, @value, @execution_date) returning id into t_id;
   INSERT INTO deposit(id, envelope_code) VALUES (t_id, @envelope_code);
   INSERT INTO operation(account_id, transaction_id, kind, value, execution_date) VALUES (@account_id, t_id, @kind, @value, @execution_date);
END $$;";

        public DepositRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {

        }

        public override Guid Add(Deposit entity)
        {
            using (var cmd = new NpgsqlCommand(INSERT_QUERY, Connection))
            {
                cmd.Parameters.AddWithValue("account_id", entity.AccountId);
                cmd.Parameters.AddWithValue("kind", entity.Kind.ToString());
                cmd.Parameters.AddWithValue("value", entity.Value);
                cmd.Parameters.AddWithValue("execution_date", entity.ExecutionDate);
                cmd.Parameters.AddWithValue("envelope_code", entity.EnvelopeCode);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Deposit client)
        {
            throw new NotSupportedException();
        }

        public override List<Deposit> FindAll()
        {
            var result = new List<Deposit>();
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, t.value, t.execution_date, d.envelope_code from transaction t inner join deposit d on t.id = d.id order by t.id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetDeposit(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Deposit FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, t.value, t.execution_date, d.envelope_code from transaction t inner join deposit d on t.id = d.id WHERE t.id = @id order by t.id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetDeposit(reader);
                        }
                    }
                }
            }

            return null;

        }

        public Deposit FindOneByEnvelopeCode(string envelopeCode)
        {
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, t.value, t.execution_date, d.envelope_code from transaction t inner join deposit d on t.id = d.id WHERE d.envelope_code = @envelop_code order by t.id", Connection))
            {
                cmd.Parameters.AddWithValue("envelope_code", envelopeCode);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetDeposit(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Deposit entity)
        {
            throw new NotSupportedException();
        }

        private Deposit GetDeposit(NpgsqlDataReader reader)
        {
            return new Deposit(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetDecimal(2),
                reader.GetDateTime(3),
                reader.GetString(4)
            );
        }
    }
}