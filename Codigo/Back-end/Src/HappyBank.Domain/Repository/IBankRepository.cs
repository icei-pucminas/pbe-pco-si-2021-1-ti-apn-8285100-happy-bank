using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IBankRepository : IRepository<Bank>
    {
        int HappyBankNumber {get;}
        Bank HappyBank{get;}

        Bank FindOneByBankNumber(int bankNumber);
    }
}