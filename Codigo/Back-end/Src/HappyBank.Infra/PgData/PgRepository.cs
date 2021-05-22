using Npgsql;
using System.Data;

namespace HappyBank.Infra.PgData
{
    public abstract class PgRepository
    {
        private NpgsqlConnection _connection;
        protected NpgsqlConnection Connection {
            get{
                if(null != this._connection && ConnectionState.Closed.Equals(_connection.State))
                {
                    _connection.Open();
                }                
                return _connection;
            }
        }
        public PgRepository(NpgsqlConnection connection)
        {
            this._connection = connection;
        }
    }
}