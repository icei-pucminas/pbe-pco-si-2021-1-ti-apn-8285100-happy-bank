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

        public DoDepositUC(IAccountRepository accountRepository, IDepositRepository deposityRepository)
        {
            this._accountRepository = accountRepository;
            this._deposityRepository = deposityRepository;
        }

        public DoDepositOutput Execute(DoDepositInput input)
        {
            var account = _accountRepository.FindOneByAgencyAndAccountNumber(input.AgencyNumber, input.AccountNumber);
            
            if(null == account)
            {
                throw new AccountNotFoundException();
            }

            var deposit = new Deposit(account.Id, input.Value, DateTime.Now, input.EnvelopeCode);
            var depositId = _deposityRepository.Add(deposit);

            return new DoDepositOutput{
                TransactionId = depositId
            };
        }
    }
}