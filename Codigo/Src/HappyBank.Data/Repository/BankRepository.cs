using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.Data.Pg;
using Npgsql;

namespace HappyBank.Data.Repository
{
    public class BankRepository : PgRepository<Bank>, IBankRepository
    {
        public BankRepository(NpgsqlConnection connection) : base(connection)
        {
        }

        public Bank HappyBank => throw new NotImplementedException();

        public override Guid Add(Bank entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Bank client)
        {
            throw new NotImplementedException();
        }

        public override List<Bank> FindAll()
        {
            throw new NotImplementedException();
        }

        public override Bank FindOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Bank client)
        {
            throw new NotImplementedException();
        }
    }
}