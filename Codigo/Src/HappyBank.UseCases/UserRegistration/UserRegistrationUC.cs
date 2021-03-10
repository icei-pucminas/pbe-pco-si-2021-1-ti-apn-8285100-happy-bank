using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Exceptions;
using System;

namespace HappyBank.UseCases.UserRegistration
{
    public class UserRegistrationUC : IUseCase<UserRegistrationInput, UserRegistrationOutput>
    {
        private const string INVALID_INPUT_MESSAGE = "Parâmetro de entrada inválido";
        private const string INVALID_USERNAME = "Nome de usuário inválido ou existente";
        private IUserRepository _userRepository;
        public UserRegistrationUC(IUserRepository userRepository) => 
            _userRepository = userRepository;

        public UserRegistrationOutput Execute(UserRegistrationInput input)
        {
            ValidateInput(input);

            return null;
        }

        private void ValidateInput(UserRegistrationInput input)
        {
            if(null == input || String.IsNullOrEmpty(input.Name) || String.IsNullOrEmpty(input.Username))
            {
                throw new ArgumentException(INVALID_INPUT_MESSAGE);
            }

            if(null != _userRepository.FindByUsername(input.Username))
            {
                throw new InvalidUsernameException(INVALID_USERNAME);
            }
        }
    }
}