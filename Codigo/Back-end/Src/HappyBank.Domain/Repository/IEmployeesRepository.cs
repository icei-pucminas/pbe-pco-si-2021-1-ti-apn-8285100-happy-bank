using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee FindOneByRegistration(string registration);   
    }
}