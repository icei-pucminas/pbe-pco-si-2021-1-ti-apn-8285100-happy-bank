using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.DummyData;

namespace HappyBank.Data.Repository
{
    public class UserRepository : DummyRepository<User>, IUserRepository
    {
        public User FindOneByUsername(string username)
        {
            return _list.Find(u => u.Username == username);
        }
    }
}