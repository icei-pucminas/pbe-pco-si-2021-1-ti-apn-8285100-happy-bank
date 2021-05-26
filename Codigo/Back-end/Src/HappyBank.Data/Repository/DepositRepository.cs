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
        private const string INSERT_TRANSACTION_QUERY = "INSERT INTO transaction(account_id, kind, value, execution_date) VALUES (@account_id, @kind, @value, @execution_date) returning id";
        private const string INSERT_DEPOSIT_QUERY = "INSERT INTO deposit(id, envelope_code) VALUES (@transaction_id, @envelope_code)";
        private const string INSERT_OPERATION_QUERY = "INSERT INTO operation(account_id, transaction_id, kind, value, execution_date) VALUES (@account_id, @transaction_id, @kind, @value, @execution_date)";

        public DepositRepository(global::Npgsql.NpgsqlConnection connection) : base(connection)
        {

        }

        public override Guid Add(Deposit entity)
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

                using (var cmd = new NpgsqlCommand(INSERT_DEPOSIT_QUERY, Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("transaction_id", transactionId);
                    cmd.Parameters.AddWithValue("envelope_code", entity.EnvelopeCode);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand(INSERT_OPERATION_QUERY, Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("transaction_id", transactionId);
                    cmd.Parameters.AddWithValue("account_id", entity.AccountId);
                    cmd.Parameters.AddWithValue("kind", (char)OperationKind.CREDIT);
                    cmd.Parameters.AddWithValue("value", entity.Value);
                    cmd.Parameters.AddWithValue("execution_date", entity.ExecutionDate);
                    cmd.Parameters.AddWithValue("envelope_code", entity.EnvelopeCode);
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
            using (var cmd = new NpgsqlCommand("select t.id, t.account_id, t.value, t.execution_date, d.envelope_code from transaction t inner join deposit d on t.id = d.id WHERE d.envelope_code = @envelope_code order by t.id", Connection))
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