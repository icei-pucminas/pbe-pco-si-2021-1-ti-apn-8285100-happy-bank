using HappyBank.Domain.Repository;
using HappyBank.Domain.Model;
using HappyBank.UseCases.Exceptions;
using HappyBank.UseCases.OpenAccount;
using NSubstitute;
using System;
using Xunit;

namespace HappyBank.UnitTests.UseCases
{
    public class OpenAccountUCTest
    {
        [Fact]
        public void Null_Input_Test_Must_Throw_ArgumentException()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();

            OpenAccountUC openAccountUC = new OpenAccountUC(userRepository, accountRepository);
            Assert.Throws<ArgumentException>(() => openAccountUC.Execute(null));            
        }

        [Fact]
        public void Empty_Input_Test_Must_Throw_ArgumentException()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();

            OpenAccountUC openAccountUC = new OpenAccountUC(userRepository, accountRepository);
            Assert.Throws<ArgumentException>(() => openAccountUC.Execute(new OpenAccountInput()));            
        }

        [Fact]
        public void No_Existing_User_Must_Throw_UserNotFoundException()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
            
            OpenAccountUC openAccountUC = new OpenAccountUC(userRepository, accountRepository);
            Assert.Throws<UserNotFoundException>(() => openAccountUC.Execute(new OpenAccountInput{
                UserId = Guid.NewGuid(),
                BranchNumber = 1
            }));            
        }

         [Fact]
        public void Existing_User_Must_Add_Account_And_Return_Its_Info()
        {
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var branchNumber = new Random().Next(9999);
            var user = new User($"User {userId}", userId.ToString());
            var account = new Account(user, branchNumber);

            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IAccountRepository accountRepository = Substitute.For<IAccountRepository>();

            accountRepository.Add(Arg.Any<Account>()).Returns(accountId);
            accountRepository.FindOne(accountId).Returns(account);
            userRepository.FindOne(userId).Returns(user);
            
            OpenAccountUC openAccountUC = new OpenAccountUC(userRepository, accountRepository);

            var output = openAccountUC.Execute(new OpenAccountInput{
                UserId = userId,
                BranchNumber = branchNumber
            });

            Assert.True(output.AccountId == accountId);
            Assert.True(output.AccountNumber != 0);
        }
    }
}