using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IDepositRepository : IRepository<Deposit>
    {
        Deposit FindOneByEnvelopeCode(string envelopeCode);
    }
}