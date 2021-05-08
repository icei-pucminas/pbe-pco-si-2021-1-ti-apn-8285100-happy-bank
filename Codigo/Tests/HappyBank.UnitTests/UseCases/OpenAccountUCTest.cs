using HappyBank.Domain.Repository;
using HappyBank.Domain.Model;
using HappyBank.UseCases.Exceptions;
using HappyBank.UseCases.OpenAccount;
using NSubstitute;
using System;
using Xunit;

namespace HappyBank.UnitTests.UseCases
{
    public class OpenAccountUCTest
    {
        [Fact]
        public void Null_Input_Test_Must_Throw_ArgumentException()
        {
            IBankRepository bankRepository = Substitute.For<IBankRepository>();
            ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            
            OpenAccountUC openAccountUC = new OpenAccountUC(bankRepository, customerRepository, accountRepository);
            Assert.Throws<ArgumentException>(() => openAccountUC.Execute(null));            
        }

        [Fact]
        public void Empty_Input_Test_Must_Throw_ArgumentException()
        {
            IBankRepository bankRepository = Substitute.For<IBankRepository>();
            ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            
            OpenAccountUC openAccountUC = new OpenAccountUC(bankRepository, customerRepository, accountRepository);
            Assert.Throws<ArgumentException>(() => openAccountUC.Execute(new OpenAccountInput()));            
        }

        [Fact]
        public void No_Existing_Customer_Must_Throw_CustomerNotFoundException()
        {
            IBankRepository bankRepository = Substitute.For<IBankRepository>();
            ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            
            bankRepository.HappyBank.Returns(new Bank(0, "HappyBank Test", "1", "", "", "", "", ""));
            
            OpenAccountUC openAccountUC = new OpenAccountUC(bankRepository, customerRepository, accountRepository);
            Assert.Throws<CustomerNotFoundException>(() => openAccountUC.Execute(new OpenAccountInput{
                CustomerId = Guid.NewGuid()
            }));            
        }

         [Fact]
        public void Existing_Customer_Must_Add_Account_And_Return_Its_Info()
        {
            var customerId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var accountNumber = new Random().Next(9999);
            var bank = Substitute.For<Bank>();
            
            var customer = new Customer(
                $"Customer {customerId}",
                customerId.ToString(),
                "Av. Amazonas", 
                "Centro", 
                "Belo Horizonte", 
                "MG", 
                "1001", 
                new DateTime(1985, 8, 15),
                "(31)91111-1111", 
                $"{customerId}@happybank.com", 
                customerId.ToString()
            );

            var account = new Account(bank, customer);

            IBankRepository bankRepository = Substitute.For<IBankRepository>();
            ICustomerRepository customerRepository = Substitute.For<ICustomerRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();

            bankRepository.HappyBank.Returns(new Bank(0, "HappyBank Test", "1", "", "", "", "", ""));

            accountRepository.Add(Arg.Any<Account>()).Returns(accountId);
            accountRepository.FindOne(accountId).Returns(account);
            customerRepository.FindOne(customerId).Returns(customer);
            
            OpenAccountUC openAccountUC = new OpenAccountUC(bankRepository, customerRepository, accountRepository);

            var output = openAccountUC.Execute(new OpenAccountInput{
                CustomerId = customerId
            });

            Assert.True(output.AccountId == accountId);
            Assert.True(output.AccountNumber != 0);
        }
    }
}