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
            var existingUsername = "existing_username";
            var existingName = "Existing Name";

            IUserRepository userRepository = Substitute.For<IUserRepository>();
            
            userRepository.FindByUsername(existingUsername).Returns(new User(existingName, existingUsername));

            UserRegistrationUC userRegistrationUC = new UserRegistrationUC(userRepository);

            var input = new UserRegistrationInput
            {
                Name = existingName,
                Username = existingUsername
            };

            Assert.Throws<InvalidUsernameException>(() => userRegistrationUC.Execute(input));
        }
    }
}