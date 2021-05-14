using System;
using Xunit;
using Npgsql;
using HappyBank.Data.Repository;
using HappyBank.Domain.Model;

namespace HappyBank.IntegrationTests.HappyData
{
    public class EmployeeRepositoryTest
    {
        private NpgsqlConnection Connection { get; set; }

        public EmployeeRepositoryTest()
        {
            var connString = "Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres;Database=happybanktests2";
            this.Connection = new NpgsqlConnection(connString);
            this.Connection.Open();

            using (var cmd = new NpgsqlCommand("DELETE FROM \"Employee\"", Connection))
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        [Fact]
        public void Null_Registration_Mus_Throw_Exception()
        {

            var repository = new EmployeeRepository(Connection);
            
            //Assert.Throws<InvalidOperationException>(() => repository.Add(new Employee(null, , 0, null, null)));
        }

        [Fact]
        public void Complete_Employee_Mus_Insert_And_Return_Id()
        {
            var repository = new EmployeeRepository(Connection);

            var id = Guid.NewGuid();
            var newId = repository.Add(CreateEmployee());
            Assert.True(newId != Guid.Empty);
        }

        [Fact]
        public void Complete_Employee_Must_Insert_And_Return_Equal_Entity()
        {
            var repository = new EmployeeRepository(Connection);
            var Employee = CreateEmployee();

            var newId = repository.Add(Employee);
            var dbEmployee = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);

            Assert.Equal(newId, dbEmployee.Id);
            Assert.Equal(Employee.Registration, dbEmployee.Registration);
            Assert.Equal(Employee.Bank, dbEmployee.Bank);
            Assert.Equal(Employee.Wage, dbEmployee.Wage);
            Assert.Equal(Employee.Name, dbEmployee.Name);
            Assert.Equal(Employee.Function, dbEmployee.Function);
        }

        [Fact]
        public void Complete_Employee_Must_Insert_And_Update_Return_Equal_Entity()
        {
            var repository = new EmployeeRepository(Connection);
            var Employee = CreateEmployee();

            var newId = repository.Add(Employee);
            var dbEmployee = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbEmployee.Id);
            Assert.Equal(Employee.Registration, dbEmployee.Registration);

            dbEmployee.Name = $"{dbEmployee.Registration} updated";
            Assert.True(repository.Update(dbEmployee));

            var updatedEmployee = repository.FindOne(newId);
            Assert.Equal(updatedEmployee.Registration, dbEmployee.Registration);
            Assert.Equal(updatedEmployee.Bank, dbEmployee.Bank);
            Assert.Equal(updatedEmployee.Wage, dbEmployee.Wage);
            Assert.Equal(updatedEmployee.Name, dbEmployee.Name);
            Assert.Equal(updatedEmployee.Function, dbEmployee.Function);
        }

        [Fact]
        public void Complete_Employee_Must_Insert_And_Not_Found_After_Delete()
        {
            var repository = new EmployeeRepository(Connection);
            var Employee = CreateEmployee();

            var newId = repository.Add(Employee);
            var dbEmployee = repository.FindOne(newId);

            Assert.True(newId != Guid.Empty);
            Assert.Equal(newId, dbEmployee.Id);
            Assert.Equal(Employee.Registration, dbEmployee.Registration);

            repository.Delete(dbEmployee);
            Assert.Null(repository.FindOne(newId));
        }

        [Fact]
        public void Complete_Employee_Must_Insert_And_Return_In_List()
        {
            var repository = new EmployeeRepository(Connection);

            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());
            repository.Add(CreateEmployee());

            var Employee = repository.FindAll();
            Assert.True(Employee.Count == 10);
        }

        [Fact]
        public void Complete_Employee_Must_Insert_And_Return_By_Registration()
        {
            var repository = new EmployeeRepository(Connection);
            var Employee = CreateEmployee();

            repository.Add(Employee);

            var dbEmployee = repository.FindOneByRegistration(Employee.Registration);
            Assert.NotNull(dbEmployee);
        }


        private Employee CreateEmployee()
        {
            var EmployeeId = Guid.NewGuid();
            var bankId = Bank.Id;
            var strId = EmployeeId.ToString().Substring(0, 20);

            return new Employee(
                strId,
                bankId,
                2.000,
                "João da Silva",
                "TI"
            );
        }
    }
}