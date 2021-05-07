using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer FindOneByEmail(string email);   
    }
}