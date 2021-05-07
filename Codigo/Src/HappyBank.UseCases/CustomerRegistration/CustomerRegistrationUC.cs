using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Constants;
using HappyBank.UseCases.Exceptions;
using System;

namespace HappyBank.UseCases.CustomerRegistration
{
    public class UserRegistrationUC : IUseCase<UserRegistrationInput, UserRegistrationOutput>
    {
        private readonly IUserRepository _userRepository;
        public UserRegistrationUC(IUserRepository userRepository) => 
            _userRepository = userRepository;

        public UserRegistrationOutput Execute(UserRegistrationInput input)
        {
            ValidateInput(input);

            var user = new User(input.Name, input.Username);

            Guid userId = _userRepository.Add(user);

            return new UserRegistrationOutput{ UserId = userId };
        }

        private void ValidateInput(UserRegistrationInput input)
        {
            if(null == input || String.IsNullOrEmpty(input.Name) || String.IsNullOrEmpty(input.Username))
            {
                throw new ArgumentException(Messages.INVALID_INPUT_MESSAGE);
            }

            if(null != _userRepository.FindOneByUsername(input.Username))
            {
                throw new InvalidUsernameException(Messages.INVALID_USERNAME);
            }
        }
    }
}