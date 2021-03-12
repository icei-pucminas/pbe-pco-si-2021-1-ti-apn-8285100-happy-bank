using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using HappyBank.Domain.Repository;
using HappyBank.UseCases.Exceptions;

namespace HappyBank.Api.Services
{
    public class ContextService
    {
        private const string USER_ID = "x-user-id";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        
        public ContextService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this._userRepository = userRepository;
            this._httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId()
        {
            var headerValues = new StringValues();
            if(_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(USER_ID, out headerValues))
            {
                Guid result = Guid.Empty;
                if(Guid.TryParse(headerValues.ToString(), out result))
                {
                    return result;
                }
            }

            throw new UserNotFoundException();
        }        
    }
}