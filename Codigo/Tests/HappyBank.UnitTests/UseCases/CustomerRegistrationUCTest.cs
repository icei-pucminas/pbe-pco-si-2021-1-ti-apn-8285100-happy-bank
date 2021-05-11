using HappyBank.Domain.Repository;
using HappyBank.Domain.Model;
using HappyBank.UseCases.Exceptions;
using HappyBank.UseCases.CustomerRegistration;
using NSubstitute;
using System;
using Xunit;

namespace HappyBank.UnitTests.UseCases
{
    
    public class CustomerRegistrationUCTest
    {
        [Fact]
        public void Null_Input_Test_Must_Throw_ArgumentException()
        {
            ICustomerRepository CustomerRepository = Substitute.For<ICustomerRepository>();

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(CustomerRepository);
            Assert.Throws<ArgumentException>(() => CustomerRegistrationUC.Execute(null));            
        }

        [Fact]
        public void Empty_Input_Test_Must_Throw_ArgumentException()
        {
            ICustomerRepository CustomerRepository = Substitute.For<ICustomerRepository>();

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(CustomerRepository);
            Assert.Throws<ArgumentException>(() => CustomerRegistrationUC.Execute(new CustomerRegistrationInput()));            
        }

        [Fact]
        public void Existing_Customer_Email_Must_Throw_ArgumentException()
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
            
            
            ICustomerRepository CustomerRepository = Substitute.For<ICustomerRepository>();
            CustomerRepository.FindOneByEmail(email).Returns(customer);

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(CustomerRepository);

            var input = new CustomerRegistrationInput
            {
                Name = name,
                GovNumber =  customerId.ToString(),
                Street = "Av. Amazonas", 
                District = "Centro", 
                City = "Belo Horizonte", 
                State = "MG", 
                AddressNumber = "1001", 
                BirthDate = new DateTime(1985, 8, 15),
                Phone = "(31)91111-1111", 
                Email = email,
                Password = customerId.ToString()
            };

            Assert.Throws<CustomerDuplicatedException>(() => CustomerRegistrationUC.Execute(input));
        }

        [Fact]
        public void No_Existin_Customername_Must_Insert_Customer_And_Return_Its_Id()
        {
            var customerId = Guid.NewGuid();
            var name = $"Customer {customerId}";
            var email = $"{customerId}@happybank.com";
            
            ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
            customerRepository.Add(Arg.Is<Customer>(x => x.Email == email)).Returns(customerId);

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(customerRepository);

            var input = new CustomerRegistrationInput
            {
                Name = name,
                Email = email
            };

            var output = CustomerRegistrationUC.Execute(input);

            Assert.NotNull(output);
            Assert.True(output.CustomerId != Guid.Empty);
        }
    }
}