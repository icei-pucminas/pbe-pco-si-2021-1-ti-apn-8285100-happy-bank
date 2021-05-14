using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class EmployeesRepositoryTest
    {
        private NpgsqlConnection Connection { get; set; }

        public EmployeesRepositoryTest()
        {
            var connString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=happybanktests2";
            this.Connection = new NpgsqlConnection(connString);
            this.Connection.Open();

            using (var cmd = new NpgsqlCommand("DELETE FROM \"employees\"", Connection))
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        [Fact]
        public void Null_Registration_Mus_Throw_Exception()
        {
            var repository = new EmployeesRepository(Connection);
            Assert.Throws<InvalidOperationException>(() => repository.Add(new Employees(null, , 0, null, null)));
        }

        [Fact]
        public void Complete_Employees_Mus_Insert_And_Return_Id()
        {
            var repository = new EmployeesRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateEmployees());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Employees_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new EmployeesRepository(Connection);
            var employees = CreateEmployees();

            var newId = repository.Add(employees);
            var dbEmployees = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbEmployees.Id);
            Assert.Equal(employees.Registration, dbEmployees.Registration);
            Assert.Equal(employees.Bank, dbEmployees.Bank);
            Assert.Equal(employees.Wage, dbEmployees.Wage);
            Assert.Equal(employees.Name, dbEmployees.Name);
            Assert.Equal(employees.Function, dbEmployees.Function);
        }

        [Fact]
        public void Complete_Employees_Must_Insert_And_Update_Return_Equal_Entity()
        {
            var repository = new EmployeesRepository(Connection);
            var employees = CreateEmployees();

            var newId = repository.Add(employees);
            var dbEmployees = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbEmployees.Id);
            Assert.Equal(employees.Registration, dbEmployees.Registration);

            dbEmployees.Name = $"{dbEmployees.Registration} updated";
            Assert.True(repository.Update(dbEmployees));

            var updatedEmployees = repository.FindOne(newId);
            Assert.Equal(updatedEmployees.Registration, dbEmployees.Registration);
            Assert.Equal(updatedEmployees.Bank, dbEmployees.Bank);
            Assert.Equal(updatedEmployees.Wage, dbEmployees.Wage);
            Assert.Equal(updatedEmployees.Name, dbEmployees.Name);
            Assert.Equal(updatedEmployees.Function, dbEmployees.Function);
        }

        [Fact]
        public void Complete_Employees_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new EmployeesRepository(Connection);
            var employees = CreateEmployees();

            var newId = repository.Add(employees);
            var dbEmployees = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbEmployees.Id);
            Assert.Equal(employees.Registration, dbEmployees.Registration);

            repository.Delete(dbEmployees);
            Assert.Null(repository.FindOne(newId));
        }

        [Fact]
        public void Complete_Employees_Must_Insert_And_Return_In_List()
        {
            var repository = new EmployeesRepository(Connection);

            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());
            repository.Add(CreateEmployees());

            var employees = repository.FindAll();
            Assert.True(employees.Count == 10);
        }

        [Fact]
        public void Complete_Employees_Must_Insert_And_Return_By_Registration()
        {
            var repository = new EmployeesRepository(Connection);
            var employees = CreateEmployees();

            repository.Add(employees);

            var dbEmployees = repository.FindOneByRegistration(employees.Registration);
            Assert.NotNull(dbEmployees);
        }


        private Employees CreateEmployees()
        {
            var employeesId = Guid.NewGuid();
            var bankId = Bank.Id;
            var strId = employeesId.ToString().Substring(0, 20);

            return new Employees(
                strId,
                bankId,
                2.000,
                "João da Silva",
                "TI"
            );
        }
    }
}