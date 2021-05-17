using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class CustomerRepositoryTest : BaseTest
    {

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
        public void Complete_Customer_Must_Insert_And_Return_Equal_Entity()
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

        [Fact]
        public void Complete_Customer_Must_Insert_And_Update_Return_Equal_Entity()
        {
            var repository = new CustomerRepository(Connection);
            var customer = CreateCustomer();

            var newId = repository.Add(customer);
            var dbCustomer = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbCustomer.Id);
            Assert.Equal(customer.Name, dbCustomer.Name);

            dbCustomer.Name = $"{dbCustomer.Name} updated";
            Assert.True(repository.Update(dbCustomer));

            var updatedCustomer = repository.FindOne(newId);

            Assert.Equal(updatedCustomer.Name, dbCustomer.Name);
            Assert.Equal(updatedCustomer.GovNumber, dbCustomer.GovNumber);
            Assert.Equal(updatedCustomer.Street, dbCustomer.Street);
            Assert.Equal(updatedCustomer.District, dbCustomer.District);
            Assert.Equal(updatedCustomer.City, dbCustomer.City);
            Assert.Equal(updatedCustomer.State, dbCustomer.State);
            Assert.Equal(updatedCustomer.AddressNumber, dbCustomer.AddressNumber);
            Assert.Equal(updatedCustomer.BirthDate, dbCustomer.BirthDate);
            Assert.Equal(updatedCustomer.Phone, dbCustomer.Phone);
            Assert.Equal(updatedCustomer.Email, dbCustomer.Email);
            Assert.Equal(updatedCustomer.Password, dbCustomer.Password);
        }

        [Fact]
        public void Complete_Customer_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new CustomerRepository(Connection);
            var customer = CreateCustomer();

            var newId = repository.Add(customer);
            var dbCustomer = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbCustomer.Id);
            Assert.Equal(customer.Name, dbCustomer.Name);

            repository.Delete(dbCustomer);
            Assert.Null(repository.FindOne(newId));
        }

        [Fact]
        public void Complete_Customer_Must_Insert_And_Return_In_List()
        {
            var repository = new CustomerRepository(Connection);

            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());
            repository.Add(CreateCustomer());


            var customers = repository.FindAll();
            Assert.True(customers.Count == 10);
        }

        [Fact]
        public void Complete_Customer_Must_Insert_And_Return_By_Email()
        {
            var repository = new CustomerRepository(Connection);
            var customer = CreateCustomer();

            repository.Add(customer);

            var dbCustomer = repository.FindOneByEmail(customer.Email);
            Assert.NotNull(dbCustomer);
        }


        private Customer CreateCustomer()
        {
            var customerId = Guid.NewGuid();
            var name = $"Customer {customerId}";
            var email = $"{customerId}@happybank.com";
            var strId = customerId.ToString().Substring(0, 6);

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