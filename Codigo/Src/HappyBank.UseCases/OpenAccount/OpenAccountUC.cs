using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Constants;
using HappyBank.UseCases.Exceptions;
using System;

namespace HappyBank.UseCases.OpenAccount
{
    public class OpenAccountUC : IUseCase<OpenAccountInput, OpenAccountOutput>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public OpenAccountUC(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public OpenAccountOutput Execute(OpenAccountInput input)
        {
            ValidateInput(input);
            
            var user = FindUser(input);

            var account = new Account(user, input.BranchNumber);
            var accountId =_accountRepository.Add(account);
            var createdAccount = _accountRepository.FindOne(accountId);

            return new OpenAccountOutput{
                AccountId = accountId,
                AccountNumber = createdAccount.AccountNumber,
                BranchNumber = createdAccount.BranchNumber
            };
        }

        private void ValidateInput(OpenAccountInput input)
        {
            if(null == input || input.UserId == Guid.Empty || input.BranchNumber <= 0)
            {
                throw new ArgumentException(Messages.INVALID_INPUT_MESSAGE);
            }
        }

        private User FindUser(OpenAccountInput input)
        {
            var user = _userRepository.FindOne(input.UserId);

            if(null == user)
            {
                throw new UserNotFoundException();
            }

            return user;
        }
    }
}