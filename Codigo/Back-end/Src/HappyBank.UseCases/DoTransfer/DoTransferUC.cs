using System;
using HappyBank.Infra.UseCases;
using HappyBank.Domain.Repository;
using HappyBank.UseCases.Exceptions;
using HappyBank.Domain.Model;

namespace HappyBank.UseCases.DoTransfer
{
    public class DoTransferUC : IUseCase<DoTransferInput, DoTransferOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITransferRepository _transferRepository;

        public DoTransferUC(IAccountRepository accountRepository, ICustomerRepository customerRepository, ITransferRepository transferRepository)
        {
            this._accountRepository = accountRepository;
            this._customerRepository = customerRepository;
            this._transferRepository = transferRepository;
        }

        public DoTransferOutput Execute(DoTransferInput input)
        {
            var customer = _customerRepository.FindOne(input.CustomerId);

            if (null == customer)
            {
                throw new CustomerNotFoundException();
            }

            var accountList = _accountRepository.FindByCustomerId(customer.Id);

            if (accountList.Count == 0)
            {
                throw new AccountNotFoundException();
            }

            var transferId = _transferRepository.Add(new Transfer(
                accountList[0].Id,
                input.AccountDestinyId,
                input.Value,
                DateTime.Now)
                );

            return new DoTransferOutput
            {
                TransactionId = transferId
            };
        }
    }
}