using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;

namespace HappyBank.UseCases.SignIn
{
    public class SignInUC : IUseCase<SignInInput, SignInOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        
        public SignInUC(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public SignInOutput Execute(SignInInput input)
        {
            var customer = _customerRepository.FindOneByEmail(input.Email);

            if(null != customer && customer.Password.Equals(input.Password))
            {
                var accountList = this._accountRepository.FindByCustomerId(customer.Id);
                if(accountList.Count > 0)
                {
                    return new SignInOutput{
                        CustomerId = customer.Id,
                        AccountNumber = accountList[0].AccountNumber,
                        AgencyNumber = accountList[0].AgencyNumber,
                        CustomerName = customer.Name,
                        CustomerEmail = customer.Email
                    };
                }
            }

            return null;
        }
    }
}