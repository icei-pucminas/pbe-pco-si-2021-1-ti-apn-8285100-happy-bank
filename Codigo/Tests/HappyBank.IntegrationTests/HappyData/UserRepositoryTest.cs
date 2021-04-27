using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class UserRepositoryTest
    {
        private NpgsqlConnection Connection {get; set;}

        public UserRepositoryTest()
        {
            var connString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=happybanktests";

            Connection = new NpgsqlConnection(connString);
        }
        
        [Fact]
        public void Null_User_Name_Mus_Throw_Exception()
        {
            var repository = new UserRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new User(null, null)));
        }

        [Fact]
        public void Complete_User_Mus_Insert_And_Return_Id()
        {
            var repository = new UserRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(new User(id.ToString(), $"User {id}"));

            Assert.NotNull(newId);
            // Assert.True(id == newId);
        }
    }
}