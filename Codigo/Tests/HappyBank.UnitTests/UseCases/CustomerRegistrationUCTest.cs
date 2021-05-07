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
        public void Existin_Customername_Must_Throw_ArgumentException()
        {
            var existingName = "Existing Name";
            var existingCustomername = "existing_Customername";
            
            ICustomerRepository CustomerRepository = Substitute.For<ICustomerRepository>();
            CustomerRepository.FindOneByCustomername(existingCustomername).Returns(new Customer(existingName, existingCustomername));

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(CustomerRepository);

            var input = new CustomerRegistrationInput
            {
                Name = existingName,
                Customername = existingCustomername
            };

            Assert.Throws<InvalidCustomernameException>(() => CustomerRegistrationUC.Execute(input));
        }

        [Fact]
        public void No_Existin_Customername_Must_Insert_Customer_And_Return_Its_Id()
        {
            var name = "New Customer";
            var Customername = "new_Customer";
            
            ICustomerRepository CustomerRepository = Substitute.For<ICustomerRepository>();
            CustomerRepository.Add(Arg.Is<Customer>(x => x.Customername == Customername)).Returns(Guid.NewGuid());

            CustomerRegistrationUC CustomerRegistrationUC = new CustomerRegistrationUC(CustomerRepository);

            var input = new CustomerRegistrationInput
            {
                Name = name,
                Customername = Customername
            };

            var output = CustomerRegistrationUC.Execute(input);

            Assert.NotNull(output);
            Assert.True(output.CustomerId != Guid.Empty);
        }
    }
}