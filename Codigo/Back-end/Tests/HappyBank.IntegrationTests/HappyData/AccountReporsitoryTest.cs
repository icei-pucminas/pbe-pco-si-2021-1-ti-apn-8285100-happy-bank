using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class AccountReporsitoryTest : BaseTest
    {
        private AccountRepository accountRepository { get; set; }
        private BankRepository bankRepository { get; set; }
        private CustomerRepository customerRepository { get; set; }

        public AccountReporsitoryTest()
        {
            this.accountRepository = new AccountRepository(this.Connection);
            this.bankRepository = new BankRepository(this.Connection);
            this.customerRepository = new CustomerRepository(this.Connection);
        }

        [Fact]
        public void Null_Registration_Mus_Throw_Exception()
        {

            var repository = new AccountRepository(Connection);
            
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Account(Guid.Empty , Guid.Empty ,1)));
        }

        [Fact]
        public void Complete_Account_Mus_Insert_And_Return_Id()
        {
            var repository = new AccountRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateAccount());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Account_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new AccountRepository(Connection);
            var Account = CreateAccount();

            var newId = repository.Add(Account);
            var dbAccount = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbAccount.Id);
            Assert.Equal(Account.BankId, dbAccount.BankId);
            Assert.Equal(Account.CustomerId, dbAccount.CustomerId);
            Assert.Equal(Account.AgencyNumber, dbAccount.AgencyNumber);
            Assert.Equal(Account.AccountNumber, dbAccount.AccountNumber);
        }

        [Fact]
        public void Complete_Account_Must_Insert_And_Update_Return_Equal_Entity()
        {
            var repository = new AccountRepository(Connection);
            var Account = CreateAccount();

            var newId = repository.Add(Account);
            var dbAccount = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbAccount.Id);
            Assert.Equal(Account.CustomerId, dbAccount.CustomerId);

            dbAccount.AccountNumber = 1234;
            Assert.True(repository.Update(dbAccount));

            var updatedAccount = repository.FindOne(newId);
            Assert.Equal(updatedAccount.CustomerId, dbAccount.CustomerId);
            Assert.Equal(updatedAccount.BankId, dbAccount.BankId);
            Assert.Equal(updatedAccount.AgencyNumber, dbAccount.AgencyNumber);
            Assert.Equal(updatedAccount.AccountNumber, dbAccount.AccountNumber);
        }

        [Fact]
        public void Complete_Account_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new AccountRepository(Connection);
            var Account = CreateAccount();

            var newId = repository.Add(Account);
            var dbAccount = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbAccount.Id);
            Assert.Equal(Account.CustomerId, dbAccount.CustomerId);

            repository.Delete(dbAccount);
            Assert.Null(repository.FindOne(newId));
        }

        [Fact]
        public void Complete_Account_Must_Insert_And_Return_In_List()
        {
            var repository = new AccountRepository(Connection);

            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());
            repository.Add(CreateAccount());

            var Account = repository.FindAll();
            Assert.True(Account.Count == 10);
        }

        [Fact]
        public void Complete_Account_Must_Insert_And_Return_By_CustomerId()
        {
            var repository = new AccountRepository(Connection);
            var Account = CreateAccount();

            repository.Add(Account);

            var dbAccount = repository.FindByCustomerId(Account.CustomerId);
            Assert.NotNull(dbAccount);
        }

        private Account CreateAccount()
        {
            var accountId = Guid.NewGuid();
            var bank = this.CreateBank();
            var customer = this.CreateCustomer();
            
            return new Account(
                accountId,
                bank.Id,
                customer.Id,
                0171,
                071010
            );
        }

        private Bank CreateBank()
        {
            var bankNumber = 171;
            var strId = Guid.NewGuid().ToString().Substring(0, 6);
            var name = $"Bank {strId}";
            
            var bank = new Bank(
                bankNumber,
                name,
                strId,
                "Av. Amazonas",
                "Centro",
                "Belo Horizonte",
                "MG",
                "1001"
            );

            var bankId = this.bankRepository.Add(bank);
            return this.bankRepository.FindOne(bankId);
        }

        private Customer CreateCustomer()
        {
            var strId = Guid.NewGuid().ToString().Substring(0, 6);
            var name = $"Customer {strId}";
            var email = $"{strId}@happybank.com";

            var customer = new Customer(
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

            var customerId = this.customerRepository.Add(customer);
            return this.customerRepository.FindOne(customerId);
        }
    }
}