using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class WithdrawRepositoryTest : BaseTest
    {
        private AccountRepository accountRepository { get; set; }
        private BankRepository bankRepository { get; set; }
        private CustomerRepository customerRepository { get; set; }
        private WithdrawRepository withdrawRepository { get; set; }

        public WithdrawRepositoryTest()
        {
            this.accountRepository = new AccountRepository(this.Connection);
            this.bankRepository = new BankRepository(this.Connection);
            this.customerRepository = new CustomerRepository(this.Connection);
            this.withdrawRepository = new WithdrawRepository(this.Connection);
        }

        [Fact]
        public void Null_Terminal_Code_Must_Throw_Exception()
        {
            var repository = new WithdrawRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Withdraw(Guid.Empty, Guid.Empty, 0m, DateTime.Now, null)));
        }

        [Fact]
        public void Complete_Withdraw_Mus_Insert_And_Return_Id()
        {
            var repository = new WithdrawRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateWithdraw());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Withdraw_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new WithdrawRepository(Connection);
            var Withdraw = CreateWithdraw();

            var newId = repository.Add(Withdraw);
            var dbWithdraw = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbWithdraw.Id);
            Assert.Equal(Withdraw.AccountId, dbWithdraw.AccountId);
            Assert.Equal(Withdraw.Value, dbWithdraw.Value);
            Assert.Equal(Withdraw.ExecutionDate, dbWithdraw.ExecutionDate);
            Assert.Equal(Withdraw.TerminalCode, dbWithdraw.TerminalCode);
        }

        [Fact]
        public void Complete_Withdraw_Must_Insert_And_Return_In_List()
        {
            var repository = new WithdrawRepository(Connection);

            repository.Add(CreateWithdraw());
            repository.Add(CreateWithdraw());
            repository.Add(CreateWithdraw());
            repository.Add(CreateWithdraw());
            repository.Add(CreateWithdraw());


            var withdraws = repository.FindAll();
            Assert.True(withdraws.Count == 5);
        }

        [Fact]
        public void Complete_Withdraw_Must_Insert_And_Return_By_Terminal_Code()
        {
            var repository = new WithdrawRepository(Connection);
            var withdraw = CreateWithdraw();

            repository.Add(withdraw);

            var dbWithdraw = repository.FindOneByTerminalCode(withdraw.TerminalCode);
            Assert.NotNull(dbWithdraw);
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


        private Withdraw CreateWithdraw()
        {
            var bank = this.CreateBank();
            var account = this.CreateAccount(bank);
            var terminalCode = Guid.NewGuid().ToString().Substring(0, 6);

            return new Withdraw(
                account.Id,
                200m,
                new DateTime(2020, 8, 15),
                terminalCode
            );
        }
    }
}