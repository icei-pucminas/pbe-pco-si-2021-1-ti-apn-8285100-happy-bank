using HappyBank.Domain.Model;
using HappyBank.Domain.Repository;
using HappyBank.Infra.UseCases;
using HappyBank.UseCases.Constants;
using HappyBank.UseCases.Exceptions;
using System;

namespace HappyBank.UseCases.CustomerRegistration
{
    public class CustomerRegistrationUC : IUseCase<CustomerRegistrationInput, CustomerRegistrationOutput>
    {
        private readonly ICustomerRepository _CustomerRepository;
        public CustomerRegistrationUC(ICustomerRepository CustomerRepository) => 
            _CustomerRepository = CustomerRepository;

        public CustomerRegistrationOutput Execute(CustomerRegistrationInput input)
        {
            ValidateInput(input);

            var Customer = new Customer(
                input.Name, 
                input.GovNumber, 
                input.Street, 
                input.District, 
                input.City, 
                input.State, 
                input.AddressNumber, 
                input.BirthDate, 
                input.Phone, 
                input.Email, 
                input.Password);

            Guid CustomerId = _CustomerRepository.Add(Customer);

            return new CustomerRegistrationOutput{ CustomerId = CustomerId };
        }

        private void ValidateInput(CustomerRegistrationInput input)
        {
            if(null == input || String.IsNullOrEmpty(input.Name) || String.IsNullOrEmpty(input.Name))
            {
                throw new ArgumentException(Messages.INVALID_INPUT_MESSAGE);
            }

            if(null != _CustomerRepository.FindOneByEmail(input.Email))
            {
                throw new CustomerNotFoundException(Messages.INVALID_USERNAME);
            }
        }
    }
}