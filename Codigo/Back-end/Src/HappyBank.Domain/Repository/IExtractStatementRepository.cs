using System;
using HappyBank.Domain.Model;
using System.Collections.Generic;

namespace HappyBank.Domain.Repository
{
    public interface IExtractStatementRepository
    {
        List<ExtractStatement> FindExtractStatement(Guid accountId, DateTime start, DateTime end);

        decimal Balance(Guid accountId);
        decimal Balance(Guid accountId, DateTime date);
    }
}