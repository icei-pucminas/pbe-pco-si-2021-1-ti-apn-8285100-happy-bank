using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class TransferRepositoryTest : BaseTest
    {
        // private TransferRepository transferRepository { get; set; }

        // public TransferRepositoryTest()
        // {
        //     this.transferRepository = new TransferRepository(this.Connection);
        // }

        // [Fact]
        // public void Null_Account_Destiny_Id_Must_Throw_Exception()
        // {
        //     var repository = new TransferRepository(Connection);
        //     Assert.Throws<InvalidOperationException>(() => repository.Add(new Transfer(Guid.Empty, Guid.Empty, Guid.Empty, 0m, DateTime.Now)));
        // }

        // [Fact]
        // public void Complete_Transfer_Mus_Insert_And_Return_Id()
        // {
        //     var repository = new TransferRepository(Connection);

        //     var id = Guid.NewGuid();
        //     var newId = repository.Add(CreateTransfer());
        //     Assert.True(newId != Guid.Empty);
        // }

        // [Fact]
        // public void Complete_Transfer_Must_Insert_And_Return_Equal_Entity()
        // {
        //     var repository = new TransferRepository(Connection);
        //     var Transfer = CreateTransfer();

        //     var newId = repository.Add(Transfer);
        //     var dbTransfer = repository.FindOne(newId);

        //     Assert.True(newId != Guid.Empty);

        //     Assert.Equal(newId, dbTransfer.Id);
        //     Assert.Equal(Transfer.AccountId, dbTransfer.AccountId);
        //     Assert.Equal(Transfer.AccountDestinyId, dbTransfer.AccountDestinyId);
        //     Assert.Equal(Transfer.Value, dbTransfer.Value);
        //     Assert.Equal(Transfer.ExecutionDate, dbTransfer.ExecutionDate);
        // }

        // [Fact]
        // public void Complete_Transfer_Must_Insert_And_Update_Return_Equal_Entity()
        // {
        //     var repository = new TransferRepository(Connection);
        //     var Transfer = CreateTransfer();

        //     var newId = repository.Add(Transfer);
        //     var dbTransfer = repository.FindOne(newId);

        //     Assert.True(newId != Guid.Empty);
        //     Assert.Equal(newId, dbTransfer.Id);
        //     Assert.Equal(Transfer.AccountDestinyId, dbTransfer.AccountDestinyId);

        //     dbTransfer.AccountDestinyId = dbTransfer.AccountDestinyId;
        //     Assert.True(repository.Update(dbTransfer));

        //     var updatedTransfer = repository.FindOne(newId);
        //     Assert.Equal(updatedTransfer.AccountId, dbTransfer.AccountId);
        //     Assert.Equal(updatedTransfer.AccountDestinyId, dbTransfer.AccountDestinyId);
        //     Assert.Equal(updatedTransfer.Value, dbTransfer.Value);
        //     Assert.Equal(updatedTransfer.ExecutionDate, dbTransfer.ExecutionDate);
        // }
        // [Fact]
        // public void Complete_Transfer_Must_Insert_And_Not_Found_After_Delete()
        // {
        //     var repository = new TransferRepository(Connection);
        //     var Transfer = CreateTransfer();

        //     var newId = repository.Add(Transfer);
        //     var dbTransfer = repository.FindOne(newId);

        //     Assert.True(newId != Guid.Empty);
        //     Assert.Equal(newId, dbTransfer.Id);
        //     Assert.Equal(Transfer.AccountDestinyId, dbTransfer.AccountDestinyId);

        //     repository.Delete(dbTransfer);
        //     Assert.Null(repository.FindOne(newId));
        // }

        // [Fact]
        // public void Complete_Transfer_Must_Insert_And_Return_In_List()
        // {
        //     var repository = new TransferRepository(Connection);

        //     repository.Add(CreateTransfer());
        //     repository.Add(CreateTransfer());
        //     repository.Add(CreateTransfer());
        //     repository.Add(CreateTransfer());
        //     repository.Add(CreateTransfer());


        //     var Transfers = repository.FindAll();
        //     Assert.True(Transfers.Count == 5);
        // }

        // [Fact]
        // public void Complete_Transfer_Must_Insert_And_Return_By_Account_Desttiny_Id()
        // {
        //     var repository = new TransferRepository(Connection);
        //     var transfer = CreateTransfer();

        //     repository.Add(transfer);

        //     var dbTransfer = repository.FindOneByAccountDestinyId(transfer.AccountDestinyId);
        //     Assert.NotNull(dbTransfer);
        // }

        // private Transfer CreateTransfer()
        // {
        //     var transferId = Guid.NewGuid();
        //     var accountId = Guid.NewGuid();
        //     var accountDestinyId = Guid.NewGuid();

        //     return new Transfer(
        //         transferId,
        //         accountId,
        //         accountDestinyId,
        //         200m,
        //         new DateTime(2020, 8, 15)
        //     );
        // }
    }
}