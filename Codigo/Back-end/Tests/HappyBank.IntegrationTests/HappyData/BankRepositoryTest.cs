using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class BankRepositoryTest : BaseTest
    {
        
        [Fact]
        public void Null_BankNumber_Mus_Throw_Exception()
        {
            var repository = new BankRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Bank(171, null, null, null, null, null, null, null)));
        }

        [Fact]
        public void Complete_Bank_Mus_Insert_And_Return_Id()
        {
            var repository = new BankRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateBank());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Bank_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new BankRepository(Connection);
            var bank = CreateBank();

            var newId = repository.Add(bank);
            var dbBank = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbBank.Id);
            Assert.Equal(bank.BankNumber, dbBank.BankNumber);
            Assert.Equal(bank.Name, dbBank.Name);
            Assert.Equal(bank.GovNumber, dbBank.GovNumber);
            Assert.Equal(bank.Street, dbBank.Street);
            Assert.Equal(bank.District, dbBank.District);
            Assert.Equal(bank.City, dbBank.City);
            Assert.Equal(bank.State, dbBank.State);
            Assert.Equal(bank.AddressNumber, dbBank.AddressNumber);
        }

        [Fact]
        public void Complete_Bank_Must_Insert_And_Update_Return_Equal_Entity()
        {
            var repository = new BankRepository(Connection);
            var bank = CreateBank();

            var newId = repository.Add(bank);
            var dbBank = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbBank.Id);
            Assert.Equal(bank.Name, dbBank.Name);

            dbBank.Name = $"{dbBank.Name} updated";
            Assert.True(repository.Update(dbBank));

            var updatedBank = repository.FindOne(newId);
            Assert.Equal(bank.BankNumber, dbBank.BankNumber);
            Assert.Equal(updatedBank.Name, dbBank.Name);
            Assert.Equal(updatedBank.GovNumber, dbBank.GovNumber);
            Assert.Equal(updatedBank.Street, dbBank.Street);
            Assert.Equal(updatedBank.District, dbBank.District);
            Assert.Equal(updatedBank.City, dbBank.City);
            Assert.Equal(updatedBank.State, dbBank.State);
            Assert.Equal(updatedBank.AddressNumber, dbBank.AddressNumber);
        }

        [Fact]
        public void Complete_Bank_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new BankRepository(Connection);
            var bank = CreateBank();

            var newId = repository.Add(bank);
            var dbBank = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbBank.Id);
            Assert.Equal(bank.Name, dbBank.Name);

            repository.Delete(dbBank);
            Assert.Null(repository.FindOne(newId));
        }

        [Fact]
        public void Complete_Bank_Must_Insert_And_Return_In_List()
        {
            var repository = new BankRepository(Connection);

            repository.Add(CreateBank());

            var bank = repository.FindAll();
            Assert.True(bank.Count == 1);
        }

        [Fact]
        public void Complete_Bank_Must_Insert_And_Return_By_BankNumber()
        {
            var repository = new BankRepository(Connection);
            var bank = CreateBank();

            repository.Add(bank);

            var dbBank = repository.FindOneByBankNumber(bank.BankNumber);
            Assert.NotNull(dbBank);
        }


        private Bank CreateBank()
        {
            var bankId = Guid.NewGuid();
            var name = $"Bank {bankId}";
            var bankNumber = 171;
            var strId = bankId.ToString().Substring(0, 20);

            return new Bank(
                bankNumber,
                name,
                strId,
                "Av. Amazonas",
                "Centro",
                "Belo Horizonte",
                "MG",
                "1001"
            );
        }
    }
}