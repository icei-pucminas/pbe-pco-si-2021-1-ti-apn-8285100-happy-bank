using System;
using Xunit;
using Npgsql;

namespace HappyBank.IntegrationTests.PgData
{
    public class PgRepositoryTests
    {
        private NpgsqlConnection Connection {get; set;}

        public PgRepositoryTests()
        {
            var connString = "Host=localhost;Username=postgres;Password=postgres;Database=happybanktests";

            Connection = new NpgsqlConnection(connString);
            Connection.Open();
        }
        
        [Fact]
        public void Null_Sample_Title_Mus_Throw_Exception()
        {
            var repository = new SampleRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new SampleEntity(Guid.NewGuid(), null)));
        }

        [Fact]
        public void Complete_Sample_Mus_Insert_And_Return_Id()
        {
            var repository = new SampleRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(new SampleEntity(id, $"Sample {id}"));

            Assert.NotNull(newId);
            Assert.True(id == newId);
        }
    }
}