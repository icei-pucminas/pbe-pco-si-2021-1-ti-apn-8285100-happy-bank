using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class TransferRepository : PgCrudRepository<Transfer>, ITransferRepository
    {
        private const string INSERT_QUERY = @"DO $$
DECLARE
   t_id uuid;
begin
   INSERT INTO transaction(account_id, kind, value, execution_date) VALUES (@account_id, @kind, @value, @execution_date) returning id into t_id;
   INSERT INTO transfer(id, account_destiny_id) VALUES (t_id, @account_destiny_id);
   INSERT INTO operation(account_id, transaction_id, kind, value, execution_date) VALUES (@account_id, t_id, @kind, @value, @execution_date);
END $$;";

        public TransferRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {

        }

        public override Guid Add(Transfer entity)
        {
            using (var cmd = new NpgsqlCommand(INSERT_QUERY, Connection))
            {
                cmd.Parameters.AddWithValue("account_id", entity.AccountId);
                cmd.Parameters.AddWithValue("account_destiny_id", entity.AccountDestinyId);
                cmd.Parameters.AddWithValue("value", entity.Value);
                cmd.Parameters.AddWithValue("execution_date", entity.ExecutionDate);
                cmd.Prepare();

                return (Guid)cmd.ExecuteScalar();
            }
        }

        public override bool Delete(Transfer client)
        {
            throw new NotSupportedException();
        }

        public override List<Transfer> FindAll()
        {
            var result = new List<Transfer>();
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer d on t.id = d.id order by t.id", Connection))
            {
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(GetTransfer(reader));
                        }
                    }
                }
            }

            return result;
        }

        public override Transfer FindOne(Guid id)
        {
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer d on t.id = d.id WHERE t.id = @id order by t.id", Connection))
            {
                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetTransfer(reader);
                        }
                    }
                }
            }

            return null;

        }

        public Transfer FindOneByAccountDestinyId(string accountDestinyId)
        {
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id,  tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer d on t.id = d.id WHERE tr.account_destiny_id = @account_destiny_id order by t.id", Connection))
            {
                cmd.Parameters.AddWithValue("account_destiny_id", accountDestinyId);
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return GetTransfer(reader);
                        }
                    }
                }
            }

            return null;
        }

        public override bool Update(Transfer entity)
        {
            throw new NotSupportedException();
        }

        private Transfer GetTransfer(NpgsqlDataReader reader)
        {
            return new Transfer(
                reader.GetGuid(0),
                reader.GetGuid(1),
                reader.GetGuid(2),
                reader.GetDecimal(3),
                reader.GetDateTime(4)
            );
        }
    }
}