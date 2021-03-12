using HappyBank.Domain.Repository;
using HappyBank.Domain.Model;
using HappyBank.UseCases.Exceptions;
using HappyBank.UseCases.UserRegistration;
using NSubstitute;
using System;
using Xunit;

namespace HappyBank.UnitTests.UseCases
{
    
    public class UserRegistrationUCTest
    {
        [Fact]
        public void Null_Input_Test_Must_Throw_ArgumentException()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();

            UserRegistrationUC userRegistrationUC = new UserRegistrationUC(userRepository);
            Assert.Throws<ArgumentException>(() => userRegistrationUC.Execute(null));            
        }

        [Fact]
        public void Empty_Input_Test_Must_Throw_ArgumentException()
        {
            IUserRepository userRepository = Substitute.For<IUserRepository>();

            UserRegistrationUC userRegistrationUC = new UserRegistrationUC(userRepository);
            Assert.Throws<ArgumentException>(() => userRegistrationUC.Execute(new UserRegistrationInput()));            
        }

        [Fact]
        public void Existin_Username_Must_Throw_ArgumentException()
        {
            var existingName = "Existing Name";
            var existingUsername = "existing_username";
            
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.FindOneByUsername(existingUsername).Returns(new User(existingName, existingUsername));

            UserRegistrationUC userRegistrationUC = new UserRegistrationUC(userRepository);

            var input = new UserRegistrationInput
            {
                Name = existingName,
                Username = existingUsername
            };

            Assert.Throws<InvalidUsernameException>(() => userRegistrationUC.Execute(input));
        }

        [Fact]
        public void No_Existin_Username_Must_Insert_User_And_Return_Its_Id()
        {
            var name = "New User";
            var username = "new_user";
            
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.Add(Arg.Is<User>(x => x.Username == username)).Returns(Guid.NewGuid());

            UserRegistrationUC userRegistrationUC = new UserRegistrationUC(userRepository);

            var input = new UserRegistrationInput
            {
                Name = name,
                Username = username
            };

            var output = userRegistrationUC.Execute(input);

            Assert.NotNull(output);
            Assert.True(output.UserId != Guid.Empty);
        }
    }
}