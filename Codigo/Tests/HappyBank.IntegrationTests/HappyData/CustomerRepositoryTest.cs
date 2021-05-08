using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class CustomerRepositoryTest
    {
        private NpgsqlConnection Connection {get; set;}

        public CustomerRepositoryTest()
        {
            var connString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=happybanktests";

            Connection = new NpgsqlConnection(connString);
        }
        
        [Fact]
        public void Null_Email_Mus_Throw_Exception()
        {
            var repository = new CustomerRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Customer(null, null, null, null, null, null, null, DateTime.Now, null, null, null)));
        }

        [Fact]
        public void Complete_Customer_Mus_Insert_And_Return_Id()
        {
            var customerId = Guid.NewGuid();
            var name = $"Customer {customerId}";
            var email = $"{customerId}@happybank.com";

            var customer = new Customer(
                name,
                customerId.ToString(),
                "Av. Amazonas", 
                "Centro", 
                "Belo Horizonte", 
                "MG", 
                "1001", 
                new DateTime(1985, 8, 15),
                "(31)91111-1111", 
                email, 
                customerId.ToString()
            );

            var repository = new CustomerRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(customer);

            Assert.NotNull(newId);
            // Assert.True(id == newId);
        }
    }
}