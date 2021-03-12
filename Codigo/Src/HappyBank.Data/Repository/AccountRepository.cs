using System;
using System.Collections.Generic;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.DummyData;

namespace HappyBank.Data.Repository
{
    public class AccountRepository : DummyRepository<Account>, IAccountRepository
    {
        public List<Account> FindByOwnerId(Guid ownerId)
        {
            return _list.FindAll(a => a.Owner.Id == ownerId);
        }
    }
}