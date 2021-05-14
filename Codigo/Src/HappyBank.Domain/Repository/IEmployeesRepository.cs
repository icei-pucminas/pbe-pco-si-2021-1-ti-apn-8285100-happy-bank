using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IEmployeesRepository : IRepository<Employees>
    {
        Customer FindOneByRegistration(string registration);   
    }
}