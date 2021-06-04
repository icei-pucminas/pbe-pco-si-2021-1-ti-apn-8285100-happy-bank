using System;
using HappyBank.Infra.UseCases;
using HappyBank.Domain.Repository;
using HappyBank.UseCases.Exceptions;
using HappyBank.Domain.Model;

namespace HappyBank.UseCases.DoDeposit
{
    public class DoDepositUC : IUseCase<DoDepositInput, DoDepositOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDepositRepository _deposityRepository;
        private readonly ICustomerRepository _customerRepository;

        public DoDepositUC(IAccountRepository accountRepository, IDepositRepository deposityRepository, ICustomerRepository customerRepository)
        {
            this._accountRepository = accountRepository;
            this._deposityRepository = deposityRepository;
            this._customerRepository = customerRepository;
        }

        public DoDepositOutput Execute(DoDepositInput input)
        {
            var account = _accountRepository.FindOneByAgencyAndAccountNumber(input.AgencyNumber, input.AccountNumber);
            
            if(null == account)
            {
                throw new AccountNotFoundException();
            }

            var customer = _customerRepository.FindOne(account.CustomerId);

            var deposit = new Deposit(account.Id, input.Value, DateTime.Now, input.EnvelopeCode);
            var depositId = _deposityRepository.Add(deposit);

            return new DoDepositOutput{
                CustomerName = customer.Name,
                TransactionId = depositId
            };
        }
    }
}