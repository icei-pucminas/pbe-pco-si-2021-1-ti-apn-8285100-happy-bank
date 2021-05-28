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
        private const string INSERT_TRANSACTION_QUERY = "INSERT INTO transaction(account_id, kind, value, execution_date) VALUES (@account_id, @kind, @value, @execution_date) returning id";
        private const string INSERT_TRANSFER_QUERY = "INSERT INTO transfer(id, account_destiny_id) VALUES (@transaction_id, @account_destiny_id)";
        private const string INSERT_OPERATION_QUERY = "INSERT INTO operation(account_id, transaction_id, kind, value, execution_date) VALUES (@account_id, @transaction_id, @kind, @value, @execution_date)";

        public TransferRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {

        }

        public override Guid Add(Transfer entity)
        {
            if(entity.AccountId == Guid.Empty)
            {
                throw new InvalidOperationException();
            }

            NpgsqlTransaction transaction = null;
            Guid transactionId = Guid.Empty;
            
            try
            {
                transaction = this.Connection.BeginTransaction();

                using (var cmd = new NpgsqlCommand(INSERT_TRANSACTION_QUERY, Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("account_id", entity.AccountId);
                    cmd.Parameters.AddWithValue("kind", (char)entity.Kind);
                    cmd.Parameters.AddWithValue("value", entity.Value);
                    cmd.Parameters.AddWithValue("execution_date", entity.ExecutionDate);
                    cmd.Prepare();

                    transactionId = (Guid)cmd.ExecuteScalar();
                }

                using (var cmd = new NpgsqlCommand(INSERT_TRANSFER_QUERY, Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("transaction_id", transactionId);
                    cmd.Parameters.AddWithValue("account_destiny_id", entity.AccountDestinyId);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand(INSERT_OPERATION_QUERY, Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("transaction_id", transactionId);
                    cmd.Parameters.AddWithValue("account_id", entity.AccountId);
                    cmd.Parameters.AddWithValue("account_destiny_id", entity.AccountDestinyId);
                    cmd.Parameters.AddWithValue("kind", (char)OperationKind.DEBIT);
                    cmd.Parameters.AddWithValue("value", entity.Value);
                    cmd.Parameters.AddWithValue("execution_date", entity.ExecutionDate);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();

                return transactionId;
            }
            catch(Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        public override bool Delete(Transfer client)
        {
            throw new NotSupportedException();
        }

        public override List<Transfer> FindAll()
        {
            var result = new List<Transfer>();
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer tr on t.id = tr.id order by t.id", Connection))
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
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer tr on t.id = tr.id WHERE t.id = @id order by t.id", Connection))
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

        public Transfer FindOneByAccountDestinyId(Guid accountDestinyId)
        {
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id,  tr.account_destiny_id, t.value, t.execution_date from transaction t inner join transfer tr on t.id = tr.id WHERE tr.account_destiny_id = @account_destiny_id order by t.id", Connection))
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