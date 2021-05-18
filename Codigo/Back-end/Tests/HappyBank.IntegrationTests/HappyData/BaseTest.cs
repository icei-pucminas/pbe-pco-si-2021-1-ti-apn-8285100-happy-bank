using System;
using Npgsql;

namespace HappyBank.IntegrationTests.HappyData
{
    public abstract class BaseTest : IDisposable
    {
        protected const string CONN_STRING = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database={0}";
        protected NpgsqlConnection Connection { get; private set; }
        protected string DatabaseName { get; private set; }
        public BaseTest()
        {
            this.CreateDatabase();
            this.SetupDatabase();
        }

        protected void CreateDatabase()
        {
            this.DatabaseName = "happybanktests_" + Guid.NewGuid().ToString().Substring(0, 8);
            this.Connection = new NpgsqlConnection(String.Format(CONN_STRING, "postgres"));
            this.Connection.Open();

            using (var cmd = new NpgsqlCommand($"CREATE DATABASE {this.DatabaseName}", this.Connection))
            {
                cmd.ExecuteNonQuery();
            }

            this.Connection.Close();
        }

        protected void SetupDatabase()
        {
            this.Connection = new NpgsqlConnection(String.Format(CONN_STRING, this.DatabaseName));
            this.Connection.Open();

            string happybankCommand = System.IO.File.ReadAllText("../../../../../Misc/happybank.sql", System.Text.Encoding.UTF8);

            using (var cmd = new NpgsqlCommand(happybankCommand, this.Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        protected void DeleteDatabase()
        {
            this.Connection.Close();
            this.Connection.Dispose();

            this.Connection = new NpgsqlConnection(String.Format(CONN_STRING, "postgres"));
            this.Connection.Open();

            var deleteDatabase = $@"
                REVOKE CONNECT ON DATABASE {this.DatabaseName} FROM public;
                SELECT pid, pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{this.DatabaseName}' AND pid <> pg_backend_pid();
                DROP DATABASE {this.DatabaseName};
            ";

            using (var cmd = new NpgsqlCommand(deleteDatabase, this.Connection))
            {
                cmd.ExecuteNonQuery();
            }

            this.Connection.Close();
        }

        public void Dispose()
        {
            this.DeleteDatabase();
        }
    }
}