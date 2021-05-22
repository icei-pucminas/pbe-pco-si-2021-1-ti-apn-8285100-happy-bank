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
        private readonly ICustomerRepository _customerRepository;
        public CustomerRegistrationUC(ICustomerRepository CustomerRepository) => 
            _customerRepository = CustomerRepository;

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

            Guid CustomerId = _customerRepository.Add(Customer);

            return new CustomerRegistrationOutput{ CustomerId = CustomerId };
        }

        private void ValidateInput(CustomerRegistrationInput input)
        {
            if(null == input || String.IsNullOrEmpty(input.Name) || String.IsNullOrEmpty(input.Name))
            {
                throw new ArgumentException(Messages.INVALID_INPUT_MESSAGE);
            }

            if(null != _customerRepository.FindOneByEmail(input.Email))
            {
                throw new CustomerDuplicatedException(Messages.INVALID_USERNAME);
            }
        }
    }
}