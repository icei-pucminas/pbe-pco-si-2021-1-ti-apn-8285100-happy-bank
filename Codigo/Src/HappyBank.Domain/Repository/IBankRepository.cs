using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IBankRepository : IRepository<Bank>
    {
        Bank HappyBank{get;}
    }
}