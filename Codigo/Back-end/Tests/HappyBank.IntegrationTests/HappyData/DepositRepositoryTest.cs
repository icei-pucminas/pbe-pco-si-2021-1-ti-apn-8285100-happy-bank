using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class DepositRepositoryTest : BaseTest
    {
        private AccountRepository accountRepository { get; set; }
        private BankRepository bankRepository { get; set; }
        private CustomerRepository customerRepository { get; set; }
        private DepositRepository depositRepository { get; set; }

        public DepositRepositoryTest()
        {
            this.accountRepository = new AccountRepository(this.Connection);
            this.bankRepository = new BankRepository(this.Connection);
            this.customerRepository = new CustomerRepository(this.Connection);
            this.depositRepository = new DepositRepository(this.Connection);
        }

        [Fact]
        public void Null_Envelope_Code_Must_Throw_Exception()
        {
            var repository = new DepositRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Deposit(Guid.Empty, Guid.Empty, 0m, DateTime.Now, null)));
        }

        [Fact]
        public void Complete_Deposit_Mus_Insert_And_Return_Id()
        {
            var repository = new DepositRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateDeposit());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Deposit_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new DepositRepository(Connection);
            var Deposit = CreateDeposit();

            var newId = repository.Add(Deposit);
            var dbDeposit = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbDeposit.Id);
            Assert.Equal(Deposit.AccountId, dbDeposit.AccountId);
            Assert.Equal(Deposit.Value, dbDeposit.Value);
            Assert.Equal(Deposit.ExecutionDate, dbDeposit.ExecutionDate);
            Assert.Equal(Deposit.EnvelopeCode, dbDeposit.EnvelopeCode);
        }

        [Fact]
        public void Complete_Deposit_Must_Insert_And_Return_In_List()
        {
            var repository = new DepositRepository(Connection);

            repository.Add(CreateDeposit());
            repository.Add(CreateDeposit());
            repository.Add(CreateDeposit());
            repository.Add(CreateDeposit());
            repository.Add(CreateDeposit());


            var deposits = repository.FindAll();
            Assert.True(deposits.Count == 5);
        }

        [Fact]
        public void Complete_Deposit_Must_Insert_And_Return_By_Envelope_Code()
        {
            var repository = new DepositRepository(Connection);
            var deposit = CreateDeposit();

            repository.Add(deposit);

            var dbDeposit = repository.FindOneByEnvelopeCode(deposit.EnvelopeCode);
            Assert.NotNull(dbDeposit);
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

        private Account CreateAccount(Bank bank)
        {
            var customer = this.CreateCustomer();
            
            var account = new Account(
                bank.Id,
                customer.Id,
                1
            );

            var accountId = this.accountRepository.Add(account);
            return this.accountRepository.FindOne(accountId);
        }


        private Deposit CreateDeposit()
        {
            var bank = this.CreateBank();
            var account = this.CreateAccount(bank);
            var envelopeCode = Guid.NewGuid().ToString().Substring(0, 6);

            return new Deposit(
                account.Id,
                200m,
                new DateTime(2020, 8, 15),
                envelopeCode
            );
        }
    }
}