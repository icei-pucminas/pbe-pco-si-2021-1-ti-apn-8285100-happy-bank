using HappyBank.Domain.Model;
using HappyBank.Infra.Data;

namespace HappyBank.Domain.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindOneByUsername(string username);   
    }
}