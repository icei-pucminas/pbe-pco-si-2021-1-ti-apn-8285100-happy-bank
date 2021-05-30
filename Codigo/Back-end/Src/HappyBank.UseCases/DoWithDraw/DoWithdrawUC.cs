using System;
using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Exceptions;

namespace HappyBank.UseCases.DoWithdraw
{
    public class DoWithdrawUC : IUseCase<DoWithdrawInput, DoWithdrawOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWithdrawRepository _withdrawRepository;
        
        public DoWithdrawUC(IAccountRepository accountRepository,
        ICustomerRepository customerRepository,
        IWithdrawRepository withdrawRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _withdrawRepository = withdrawRepository;
        }

        public DoWithdrawOutput Execute(DoWithdrawInput input)
        {
            var customer = _customerRepository.FindOne(input.CustomerId);

            if(null == customer)
            {
                throw new CustomerNotFoundException();
            }

            var accountList = _accountRepository.FindByCustomerId(customer.Id);
            
            if(accountList.Count == 0)
            {
                throw new AccountNotFoundException();
            }

            var withdrawId = _withdrawRepository.Add(new Withdraw(
                accountList[0].Id, 
                input.Value, 
                DateTime.Now, 
                input.TerminalCode)
            );
            
            return new DoWithdrawOutput{
                TransactionId = withdrawId
            };
        }
    }
}