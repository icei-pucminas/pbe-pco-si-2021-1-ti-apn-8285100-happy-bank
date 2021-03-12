using HappyBank.Domain.Model;
using HappyBank.Infra.Data;
using System;
using System.Collections.Generic;

namespace HappyBank.Domain.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        List<Account> FindByOwnerId(Guid ownerId);
    }
}