using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Transfer FindOneByAccountDestinyId(string accountDestinyId);
    }
}