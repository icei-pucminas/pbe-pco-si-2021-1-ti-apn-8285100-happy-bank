using System;
using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Exceptions;

namespace HappyBank.UseCases.FindAccount
{
    public class FindAccountUC : IUseCase<FindAccountInput, FindAccountOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ICustomerRepository _customerRepository;

        public FindAccountUC(IAccountRepository accountRepository, IBankRepository bankRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _bankRepository = bankRepository;
            _customerRepository = customerRepository;
        }

        public FindAccountOutput Execute(FindAccountInput input)
        {
            Guid bankId = input.BankId.HasValue ? input.BankId.Value : _bankRepository.HappyBank.Id;

            var account = _accountRepository.FindOneByAgencyAndAccountNumber(bankId, input.AgencyNumber, input.AccountNumber);

            if(null == account)
            {
                throw new AccountNotFoundException();
            }

            var customer = _customerRepository.FindOne(account.CustomerId);

            return new FindAccountOutput{
                AccountId = account.Id,
                CustomerName = customer.Name,
                GovNumber = customer.GovNumber
            };
        }
    }
}