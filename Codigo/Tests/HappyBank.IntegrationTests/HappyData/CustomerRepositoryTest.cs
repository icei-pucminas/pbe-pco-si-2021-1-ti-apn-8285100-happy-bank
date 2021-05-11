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
            var repository = new CustomerRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateCustomer());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Customer_Mus_Insert_And_Return_Equal_Entity()
        {
            var repository = new CustomerRepository(Connection);
            var customer = CreateCustomer();

            var newId = repository.Add(customer);
            var dbCustomer = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            
            Assert.Equal(newId, dbCustomer.Id);
            Assert.Equal(customer.Name, dbCustomer.Name);
            Assert.Equal(customer.GovNumber, dbCustomer.GovNumber);
            Assert.Equal(customer.Street, dbCustomer.Street);
            Assert.Equal(customer.District, dbCustomer.District);
            Assert.Equal(customer.City, dbCustomer.City);
            Assert.Equal(customer.State, dbCustomer.State);
            Assert.Equal(customer.AddressNumber, dbCustomer.AddressNumber);
            Assert.Equal(customer.BirthDate, dbCustomer.BirthDate);
            Assert.Equal(customer.Phone, dbCustomer.Phone);
            Assert.Equal(customer.Email, dbCustomer.Email);
            Assert.Equal(customer.Password, dbCustomer.Password);
        }

        private Customer CreateCustomer()
        {
            var customerId = Guid.NewGuid();
            var name = $"Customer {customerId}";
            var email = $"{customerId}@happybank.com";
            var strId = customerId.ToString().Substring(0, 20);

            return new Customer(
                name,
                strId,
                "Av. Amazonas", 
                "Centro", 
                "Belo Horizonte", 
                "MG", 
                "1001", 
                new DateTime(1985, 8, 15),
                "(31)91111-1111", 
                email, 
                strId
            );
        }
    }
}