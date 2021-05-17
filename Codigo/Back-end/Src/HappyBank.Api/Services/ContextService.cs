using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using HappyBank.Domain.Repository;
using HappyBank.UseCases.Exceptions;

namespace HappyBank.Api.Services
{
    public class ContextService
    {
        private const string CUSTOMER_ID = "x-customer-id";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        
        public ContextService(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._customerRepository = customerRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public Guid CustomerId()
        {
            var headerValues = new StringValues();
            if(_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CUSTOMER_ID, out headerValues))
            {
                Guid result = Guid.Empty;
                if(Guid.TryParse(headerValues.ToString(), out result))
                {
                    return result;
                }
            }

            throw new CustomerNotFoundException();
        }        
    }
}