using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class DepositRepositoryTest : BaseTest
    {
        private DepositRepository depositRepository { get; set; }

        public DepositRepositoryTest()
        {
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
        public void Complete_Deposit_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new DepositRepository(Connection);
            var Deposit = CreateDeposit();

            var newId = repository.Add(Deposit);
            var dbDeposit = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbDeposit.Id);
            Assert.Equal(Deposit.EnvelopeCode, dbDeposit.EnvelopeCode);

            repository.Delete(dbDeposit);
            Assert.Null(repository.FindOne(newId));
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

        private Deposit CreateDeposit()
        {
            var depositId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var envelopeCode = depositId.ToString().Substring(0, 6);

            return new Deposit(
                depositId,
                accountId,
                200m,
                new DateTime(2020, 8, 15),
                envelopeCode
            );
        }
    }
}