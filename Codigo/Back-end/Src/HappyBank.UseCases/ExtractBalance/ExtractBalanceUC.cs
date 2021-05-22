using HappyBank.Infra.UseCases;
using HappyBank.Domain.Repository;

namespace HappyBank.UseCases.ExtractBalance
{
    public class ExtractBalanceUC : IUseCase<ExtractBalanceInput, decimal>
    {
        private readonly IExtractStatementRepository _extractStatementRepository;
        private readonly IAccountRepository _accountRepository;

        public ExtractBalanceUC(IAccountRepository accountRepository, IExtractStatementRepository extractStatementRepository)
        {
            _accountRepository = accountRepository;
            _extractStatementRepository = extractStatementRepository;
        }

        public decimal Execute(ExtractBalanceInput input)
        {
            var accountList = this._accountRepository.FindByCustomerId(input.CustomerId);
            var result = 0m;

            if(accountList.Count > 0)
            {
                var accountId = accountList[0].Id;
                if(input.Date.HasValue)
                {
                    result = _extractStatementRepository.Balance(accountId, input.Date.Value);
                }else{
                    result = _extractStatementRepository.Balance(accountId);
                }
            }

            return result;
        }
    }
}