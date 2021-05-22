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
        private readonly IBankRepository _bankRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public OpenAccountUC(IBankRepository bankRepository, ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _bankRepository = bankRepository;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public OpenAccountOutput Execute(OpenAccountInput input)
        {
            ValidateInput(input);
            
            var Customer = FindCustomer(input);

            var account = new Account(_bankRepository.HappyBank.Id, Customer.Id, 1);
            var accountId =_accountRepository.Add(account);
            var createdAccount = _accountRepository.FindOne(accountId);

            return new OpenAccountOutput{
                AccountId = accountId,
                AccountNumber = createdAccount.AccountNumber,
                AgencyNumber = createdAccount.AgencyNumber
            };
        }

        private void ValidateInput(OpenAccountInput input)
        {
            if(null == input || input.CustomerId == Guid.Empty)
            {
                throw new ArgumentException(Messages.INVALID_INPUT_MESSAGE);
            }
        }

        private Customer FindCustomer(OpenAccountInput input)
        {
            var Customer = _customerRepository.FindOne(input.CustomerId);

            if(null == Customer)
            {
                throw new CustomerNotFoundException();
            }

            return Customer;
        }
    }
}