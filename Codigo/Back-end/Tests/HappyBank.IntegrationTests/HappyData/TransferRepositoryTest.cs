using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class TransferRepositoryTest : BaseTest
    {
        private AccountRepository accountRepository { get; set; }
        private BankRepository bankRepository { get; set; }
        private CustomerRepository customerRepository { get; set; }
        private TransferRepository transferRepository { get; set; }

        public TransferRepositoryTest()
        {
            this.accountRepository = new AccountRepository(this.Connection);
            this.bankRepository = new BankRepository(this.Connection);
            this.customerRepository = new CustomerRepository(this.Connection);
            this.transferRepository = new TransferRepository(this.Connection);
        }

        [Fact]
        public void Null_Account_Destiny_Id_Must_Throw_Exception()
        {
            var repository = new TransferRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Transfer(Guid.Empty, Guid.Empty, Guid.Empty, 0m, DateTime.Now)));
        }

        [Fact]
        public void Complete_Transfer_Mus_Insert_And_Return_Id()
        {
            var repository = new TransferRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateTransfer());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Transfer_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new TransferRepository(Connection);
            var Transfer = CreateTransfer();

            var newId = repository.Add(Transfer);
            var dbTransfer = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbTransfer.Id);
            Assert.Equal(Transfer.AccountId, dbTransfer.AccountId);
            Assert.Equal(Transfer.AccountDestinyId, dbTransfer.AccountDestinyId);
            Assert.Equal(Transfer.Value, dbTransfer.Value);
            Assert.Equal(Transfer.ExecutionDate, dbTransfer.ExecutionDate);
        }

        [Fact]
        public void Complete_Transfer_Must_Insert_And_Return_In_List()
        {
            var repository = new TransferRepository(Connection);

            repository.Add(CreateTransfer());
            repository.Add(CreateTransfer());
            repository.Add(CreateTransfer());
            repository.Add(CreateTransfer());
            repository.Add(CreateTransfer());


            var transfers = repository.FindAll();
            Assert.True(transfers.Count == 5);
        }

        [Fact]
        public void Complete_Transfer_Must_Insert_And_Return_By_Account_Desttiny_Id()
        {
            var repository = new TransferRepository(Connection);
            var transfer = CreateTransfer();

            repository.Add(transfer);

            var dbTransfer = repository.FindOneByAccountDestinyId(transfer.AccountDestinyId);
            Assert.NotNull(dbTransfer);
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

        private Transfer CreateTransfer()
        {
            var bank = this.CreateBank();
            var account = this.CreateAccount(bank);
            var accountDestiny = this.CreateAccount(bank);

            return new Transfer(
                account.Id,
                accountDestiny.Id,
                200m,
                new DateTime(2020, 8, 15)
            );
        }
    }
}