using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IWithdrawRepository : IRepository<Withdraw>
    {
        Withdraw FindOneByTerminalCode(string terminalCode);
    }
}