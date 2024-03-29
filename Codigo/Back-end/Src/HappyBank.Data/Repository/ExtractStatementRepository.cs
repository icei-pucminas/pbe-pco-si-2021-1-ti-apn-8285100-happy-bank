using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.PgData;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class ExtractStatementRepository : PgRepository, IExtractStatementRepository
    {
        private const string EXTRACT_STATEMENT_QUERY = @"
            SELECT
                '00000000-0000-0000-0000-000000000000' id,
                'SALDO ANTERIOR' as description,
                @start - INTERVAL '1 DAY' as execution_date,
                coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) as value
            FROM
                operation o
            WHERE
                account_id = @account_id
                AND execution_date < @start
   
            UNION ALL
 
            SELECT
                t.id,
                case
                    when t.kind = 'd' then 'DEPÓSITO'
                    when t.kind = 't' then 'TRANSFERÊNCIA'
                    when t.kind = 'w' then 'SAQUE'
                end as description,
                t.execution_date,
                coalesce(sum(case when o.kind = 'c' then o.value else o.value * -1 end), 0) as value
            FROM
                operation o
            INNER JOIN transaction t ON
                o.transaction_id = t.id
            WHERE
                o.account_id = @account_id
                AND t.execution_date between @start and @end
            GROUP BY
                t.id,
                t.kind

            UNION ALL
  
            SELECT
                '00000000-0000-0000-0000-000000000000' id,
                'SALDO EM ' || to_char(@end, 'DD/MM/YYYY') as description,
                @end as execution_date,
                coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) as value
            FROM
                operation o
            WHERE
                account_id = @account_id
                AND o.execution_date <= @end
                
            ORDER BY 3";

         private const string BALANCE_QUERY = @"
            SELECT coalesce(sum(case when kind = 'c' then value else value * -1 end), 0) as value
            FROM
                operation o
            WHERE
                account_id = @account_id
                AND execution_date <= @date";

        public ExtractStatementRepository(NpgsqlConnection connection) : base(connection)
        {

        }

        public decimal Balance(Guid accountId)
        {
            return Balance(accountId, DateTime.Now);
        }

        public decimal Balance(Guid accountId, DateTime date)
        {
            var extractStatement = new List<ExtractStatement>();

            using (var cmd = new NpgsqlCommand(BALANCE_QUERY, Connection))
            {
                cmd.Parameters.AddWithValue("account_id", accountId);
                cmd.Parameters.AddWithValue("date", date);
                
                cmd.Prepare();

                return (decimal)cmd.ExecuteScalar();
            }
        }

        public List<ExtractStatement> FindExtractStatement(Guid accountId, DateTime start, DateTime end)
        {
            var extractStatement = new List<ExtractStatement>();

            using (var cmd = new NpgsqlCommand(EXTRACT_STATEMENT_QUERY, Connection))
            {
                cmd.Parameters.AddWithValue("account_id", accountId);
                cmd.Parameters.AddWithValue("start", start.Date);
                cmd.Parameters.AddWithValue("end", end.AddDays(1).AddTicks(-1)); //Force time to final of the day
                cmd.Prepare();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            extractStatement.Add(GeExtractStatement(reader));
                        }
                    }
                }
            }

            return extractStatement;
        }

        private ExtractStatement GeExtractStatement(NpgsqlDataReader reader)
        {
            return new ExtractStatement{
                Id = reader.GetGuid(0),
                Description = reader.GetString(1),
                ExecutionDate = reader.GetDateTime(2),
                Value = reader.GetDecimal(3)
            };
        }
    }
}